

window.onload = function () {

    document.getElementById("orderNowBtn")
        .addEventListener("click", confirmAdmin);
}

function confirmAdmin() {

    $.ajax({
        url: "https://localhost:44301/AdminCart/CartAdded",
        method: "GET",
        success: (data) => {
            if (data != "[]") {
                shoppingCart.clearCart();
                loadCart();
                loadCart();
                alert("Thank You \nYour order has been recorded");
            } else {
                alert("Sorry, Your order does not have any product yet");
            }
        },
        error: (e) => { console.log(e) }
    });
}