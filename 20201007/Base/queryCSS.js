// Limpiar el estilo previamente aplicado
(() => {
    const body = document.querySelector('body');
    body.insertAdjacentHTML('afterbegin', '<input id="ruleSelector" type="text" placeholder="Type selector" /><hr /><textarea id="ruleDeclaration" rows="10" placeholder="Type property : values set"></textarea>');

    const ruleSelector = document.getElementById('ruleSelector');
    const ruleDeclaration = document.getElementById('ruleDeclaration');

    ruleSelector.addEventListener("keyup", (event) => {
        applyStyle();
    });

    ruleDeclaration.addEventListener("keyup", (event) => {
        applyStyle();
    });

    function applyStyle(target) {
        // Limpiar reglas previas
        let previas = document.querySelectorAll('[style]');
        previas.forEach((node) => { node.attributes.removeNamedItem('style') });

        // Aplicar nueva regla
        let selector = ruleSelector?.value;
        let declaration = ruleDeclaration?.value;
        if (selector && declaration) {
            let nodes = document.querySelectorAll(selector);
            if (nodes) {
                nodes.forEach((node) => {
                    console.log(node);
                    console.log(declaration);
                    node.setAttribute("style", declaration);
                });
            }
        }
    }
})();