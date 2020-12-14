// Se crea el manejador el evento para cuando se efectuan cambios de datos
let inputs = document.querySelectorAll('.form-control');
inputs.forEach(input => {
    input.addEventListener('change', (event) => {
        let prop = event.target.name;
        // Se agrega evidencia del cambio para ser enviado como parte del POST
        // Se confirma que no se haya agregado previamente
        if (!document.querySelector(`input[type='hidden'][value='${prop}']`)) {
            let input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'ModifiedProperties';
            input.value = prop;
            event.target.parentElement.appendChild(input);
        }
    });
});