// Game rendering functionality
function renderGameContent(paragraphs, options) {
    const $textContainer = $('#game-text');
    const $optionsContainer = $('#dialogue-options');

    // Clear previous content
    $textContainer.empty();
    $optionsContainer.empty();

    // Render paragraphs with fade-in animation
    if (paragraphs && paragraphs.length > 0) {
        paragraphs.forEach((paragraph, index) => {
            const $p = $('<p>').text(paragraph).hide();
            $textContainer.append($p);
            $p.fadeIn(300 + (index * 100));
        });
    }

    // Render dialogue options (up to 5) with fade-in animation
    if (options && options.length > 0) {
        const maxOptions = Math.min(options.length, 5);
        for (let i = 0; i < maxOptions; i++) {
            const $button = $('<button>')
                .text(options[i])
                .addClass('dialogue-option')
                .hide()
                .on('click', () => {
                    console.log('Selected option:', options[i]);
                });
            $optionsContainer.append($button);
            $button.fadeIn(300 + (i * 100));
        }
    }
}

// Convert DialogueModel to paragraphs and options arrays
function convertDialogueModel(dialogueModel) {
    const paragraphs = dialogueModel.paragraphs || [];
    
    const options = [];
    if (dialogueModel.a) options.push(dialogueModel.a);
    if (dialogueModel.b) options.push(dialogueModel.b);
    if (dialogueModel.c) options.push(dialogueModel.c);
    if (dialogueModel.d) options.push(dialogueModel.d);
    if (dialogueModel.e) options.push(dialogueModel.e);
    
    return { paragraphs, options };
}

// Fetch dialogue from API
function fetchDialogue(characterName, onSuccess, onError) {
    $.get(`/api/dialogue/${encodeURIComponent(characterName)}`)
        .done(function(data) {
            if (onSuccess) onSuccess(data);
        })
        .fail(function() {
            if (onError) onError();
        });
}

// Initialize game page when loaded
$(document).ready(function() {
    const $gameContainer = $('.game-container');
    if ($gameContainer.length) {
        const characterName = $gameContainer.data('character-name');
        
        if (characterName) {
            fetchDialogue(characterName, function(dialogueModel) {
                const { paragraphs, options } = convertDialogueModel(dialogueModel);
                renderGameContent(paragraphs, options);
            }, function() {
                // Fallback to default content if no dialogue found
                const defaultParagraphs = [
                    'You find yourself standing in a misty forest.',
                    'The trees loom tall around you, their branches disappearing into the fog.',
                    'A narrow path winds through the undergrowth ahead.'
                ];
                const defaultOptions = [
                    'Follow the path',
                    'Explore the trees',
                    'Call out',
                    'Turn back',
                    'Wait silently'
                ];
                renderGameContent(defaultParagraphs, defaultOptions);
            });
        } else {
            // No character name, show default content
            const defaultParagraphs = [
                'You find yourself standing in a misty forest.',
                'The trees loom tall around you, their branches disappearing into the fog.',
                'A narrow path winds through the undergrowth ahead.'
            ];
            const defaultOptions = [
                'Follow the path',
                'Explore the trees',
                'Call out',
                'Turn back',
                'Wait silently'
            ];
            renderGameContent(defaultParagraphs, defaultOptions);
        }
    }
});
