let form = document.querySelector("#form"); //almacena el formulario
let boton = document.querySelector("#boton");//y el boton 

function Habilitar() {

    let desabilitar = false;

    if (form.email.value == "") {
        desabilitar = true;//cambia el valor

    }
    if (form.contra.value == "") {
        desabilitar = true;//cambia el valor

    }
   
    if (desabilitar == true) {
        boton.disabled = true;


    }
    else {
        boton.disabled = false;


    }

}

form.addEventListener("keyup", Habilitar) //evento para verificar que el usuario esta escribiendo