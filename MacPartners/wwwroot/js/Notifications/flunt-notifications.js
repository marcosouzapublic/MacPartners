function showFluntNotifications(notifications, action, objectName) {
    Swal.fire(
        'Falha ao ' + action + ' ' + objectName + '.',
        notifications,
        'error'
    )
}