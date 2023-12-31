﻿


checkAuthorization();

function checkAuthorization() {
    $.ajax({
        method: "GET",
        url: "https://localhost:44301/Home/IsAuthorized",
        dataType: "json",
        success: (success) => {

            //let isAuthorizeObj = JSON.parse(success);
            console.log(success.isAuthorized);
            isauthorize = success.isAuthorized;
            cartButtonsEvents(isauthorize);
        },
        error: (error) => { console.log(error); }
    });
}

function saveCartOnServer(data) {
    $.ajax({
        method: "POST",
        url: "https://localhost:44301/Home/Add",
        contentType: "application/json",
        dataType: "json",
        data: data,
        success: (success) => { console.log(success) },
        error: (error) => { console.log(error); }
    });
}

function loadCartFromServer(data) {
    $.ajax({
        method: "GET",
        url: "https://localhost:44301/Home/Get",
        dataType: "json",
        success: (success) => { console.log(success) },
        error: (error) => { console.log(error); }
    });
}



// ************************************************
// Shopping Cart API
// ************************************************



// Save cart
function saveCart() {
    localStorage.setItem('shoppingCart', JSON.stringify(cart));
    saveCartOnServer(JSON.stringify(cart));
}

// Load cart
function loadCart() {
    cart = JSON.parse(localStorage.getItem('shoppingCart'));
    loadCartFromServer();
}

var shoppingCart = (function shoppingCartFun() {
    // =============================
    // Private methods and propeties
    // =============================
    cart = [];

    // Constructor
    function Item(name, price, count) {
        this.name = name;
        this.price = price;
        this.count = count;
    }

    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
    obj.addItemToCart = function (name, price, count) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart[item].count++;
                saveCart();
                return;
            }
        }
        var item = new Item(name, price, count);
        cart.push(item);
        saveCart();
        //fun(JSON.stringify(cart));
    }

    // Set count from item
    obj.setCountForItem = function (name, count) {
        for (var i in cart) {
            if (cart[i].name === name) {
                cart[i].count = count;
                break;
            }
        }
    };

    // Remove item from cart
    obj.removeItemFromCart = function (name) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart[item].count--;
                if (cart[item].count === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (name) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart.splice(item, 1);
                break;
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
    }

    // Count cart 
    obj.totalCount = function () {
        var totalCount = 0;
        for (var item in cart) {
            totalCount += cart[item].count;
        }
        return totalCount;
    }

    // Total cart
    obj.totalCart = function () {
        var totalCart = 0;
        for (var item in cart) {
            totalCart += cart[item].price * cart[item].count;
        }
        return Number(totalCart.toFixed(2));
    }

    // List cart
    obj.listCart = function () {
        var cartCopy = [];
        for (i in cart) {
            item = cart[i];
            itemCopy = {};
            for (p in item) {
                itemCopy[p] = item[p];

            }
            itemCopy.total = Number(item.price * item.count).toFixed(2);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();

function cartButtonsEvents(isauthorize) {

    if (localStorage.getItem("shoppingCart") != null && isauthorize == true) {
        loadCart();
    }


    // Add item
    $('.add-to-cart').click(function (event) {
        event.preventDefault();
        var name = $(this).data('name');
        var price = Number($(this).data('price'));

        if (isauthorize) {
            shoppingCart.addItemToCart(name, price, 1);

            displayCart();
        } else {
            location.href = "https://localhost:44301/Account/Login";
        }


        displayCart();
    });

    // Clear items
    $('.clear-cart').click(function () {
        if (isauthorize) {
            shoppingCart.clearCart();
            displayCart();
        }
        else {
            location.href = "https://localhost:44301/Account/Login";
        }
    });



}

// *****************************************
// Triggers / Events
// ***************************************** 





function displayCart() {
    var cartArray = shoppingCart.listCart();
    var output = "";
    for (var i in cartArray) {
        output += "<tr>"
            + "<td>" + cartArray[i].name + "</td>"
            + "<td>(" + cartArray[i].price + ")</td>"
            + "<td><div class='input-group'><button class='minus-item input-group-addon btn btn-primary' data-name=" + cartArray[i].name + ">-</button>"
            + "<input type='number' class='item-count form-control' data-name='" + cartArray[i].name + "' value='" + cartArray[i].count + "'>"
            + "<button class='plus-item btn btn-primary input-group-addon' data-name=" + cartArray[i].name + ">+</button></div></td>"
            + "<td><button class='delete-item btn btn-danger' data-name=" + cartArray[i].name + ">X</button></td>"
            + " = "
            + "<td>" + cartArray[i].total + "</td>"
            + "</tr>";
    }
    $('.show-cart').html(output);
    $('.total-cart').html(shoppingCart.totalCart());
    $('.total-count').html(shoppingCart.totalCount());
}

// Delete item button

$('.show-cart').on("click", ".delete-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.removeItemFromCartAll(name);
    displayCart();
})

// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.removeItemFromCart(name);
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.addItemToCart(name);
    displayCart();
})

// Item count input
$('.show-cart').on("change", ".item-count", function (event) {
    var name = $(this).data('name');
    var count = Number($(this).val());
    shoppingCart.setCountForItem(name, count);
    displayCart();
});

displayCart();
