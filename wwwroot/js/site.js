// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showNotification(message, type = "success") {
    // Daha önceki notification varsa sil
    const existing = document.getElementById("custom-alert");
    if (existing) existing.remove();

    // Yeni notification öğesi
    const alert = document.createElement("div");
    alert.id = "custom-alert";
    alert.className = `alert alert-${type} shadow`;
    alert.style.position = "fixed";
    alert.style.top = "20px";
    alert.style.left = "50%";
    alert.style.transform = "translateX(-50%)";
    alert.style.zIndex = "1050";
    alert.style.padding = "1rem 2rem";
    alert.style.borderRadius = "0.5rem";
    alert.style.fontSize = "1rem";
    alert.style.fontWeight = "500";
    alert.style.boxShadow = "0 4px 10px rgba(0,0,0,0.1)";
    alert.style.transition = "opacity 0.5s";
    alert.innerText = message;

    document.body.appendChild(alert);

    // 3 saniye sonra fade out ve sil
    setTimeout(() => {
        alert.style.opacity = "0";
        setTimeout(() => alert.remove(), 500);
    }, 3000);
}

// Modal dinamik konumlandırma
/* Modal dinamik konumlandırma */
document.addEventListener('DOMContentLoaded', function() {
    const modals = document.querySelectorAll('.modal-dialog.modal-dialog-centered');
    modals.forEach(modal => {
      modal.addEventListener('show.bs.modal', function () {
        const viewportHeight = window.innerHeight;
        const modalHeight = modal.offsetHeight;
        const topOffset = (viewportHeight - modalHeight) / 2;
        modal.style.top = topOffset + "px";
        modal.style.left = "50%";
        modal.style.transform = "translateX(-50%)";
      });
    });
  });
  

