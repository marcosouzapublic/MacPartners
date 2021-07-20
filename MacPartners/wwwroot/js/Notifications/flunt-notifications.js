function showFluntNotifications(notifications, action, objectName) {
    Swal.fire(
        'Falha ao ' + action + ' ' + objectName + '.',
        notifications,
        'error'
    )
}


function showSuccessMessage(message) {
    Swal.fire(
        'Pronto!',
        message,
        'success'
    )
}