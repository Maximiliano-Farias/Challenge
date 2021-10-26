

function EditarCrear(id = "") {
	var direccion = "/Usuario/EditCreate";
	$.ajax({
		url: direccion,
		type: "put",
		data: $.param({ "idUser": id }),
		error: function (hr) {
			alert("Error al intentar eliminar el registro");
		},
		success: function (html) {
			alert("Usuario borrado correctamente");
			location.reload(false)
		}

	});
};

function DeleteUser(id) {
	var opcion = confirm("¿Esta Seguro que desea eliminar el usuario?");
	if (opcion == true) {
		var direccion = "/Usuario/DeleteUser";
		$.ajax({
			url: direccion,
			type: "delete",
			data: $.param({ "idUser": id }),
			error: function (hr) {
				alert("Error al intentar eliminar el registro");
			},
			success: function (html) {
				alert("Usuario borrado correctamente");
				location.reload(false)
			}

		});
	}
	
}

if($('#cancelBtn').length > 0) {
document.getElementById('cancelBtn').onclick = function () {
	document.location.href = "/";
	}
}




document.getElementById('guardarBtn').onclick = function () {

	var idUser = document.getElementById("idUser").value;
	var userName = document.getElementById("userName").value;
	if (userName == "") {
		alertarDato();
		document.getElementById("userName").focus();
		return;
	}
	var nombre = document.getElementById("nombre").value;
	if (nombre == "") {
		alertarDato();
		document.getElementById("nombre").focus();
		return;
	}
	var email = document.getElementById("email").value;
	if (email == "") {
		alertarDato();
		document.getElementById("email").focus();
		return;
	}
	var telefono = document.getElementById("telefono").value;
	//verifico formato de mail
	campo = document.getElementById('email').value;
	emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
	if (!emailRegex.test(campo)) {
		alert("El mail no tiene un formato correcto");
		document.getElementById("email").focus();
		return;
	}

	if (idUser == 0) {
		//es un nuevo usuario para crear
		var direccion = "/Usuario/CreateUser";
		$.ajax({
			url: direccion,
			type: "post",
			data: $.param({ "userName": userName, "nombre": nombre, "email": email, "telefono": telefono }),
			error: function (hr) {
				alert("Error al intentar crear usuario: " + hr.responseText);
			},
			success: function (html) {
				alert("Usuario creado correctamente");
			}

		});
	}
	else {
		//es un update de un existente
		var direccion = "/Usuario/UpdateUser";
		$.ajax({
			url: direccion,
			type: "put",
			data: $.param({ "id": idUser,"userName": userName, "nombre": nombre, "email": email, "telefono": telefono }),
			error: function (hr) {
				alert("Error al intentar modificar usuario: " + hr.responseText);
			},
			success: function (html) {
				alert("Usuario modificado correctamente");
				
			}

		});
    }
}


function isNumberKey(evt) {
	var charCode = (evt.which) ? evt.which : evt.keyCode
	if (charCode > 31 && (charCode < 48 || charCode > 57))
		return false;
	return true;
}

document.getElementById('email').addEventListener('input', function () {
	campo = event.target;
	correcto = document.getElementById('emailCorrecto');
	emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
	if (emailRegex.test(campo.value)) {
		correcto.innerText = '  OK';
	} else {
		correcto.innerText = "  X";
	}
});


function alertarDato(){
	alert("Dato completar dato");
}


