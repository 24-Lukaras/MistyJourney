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

// Initialize game page when loaded
$(document).ready(function() {
    if ($('#game-text').length) {
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
});
