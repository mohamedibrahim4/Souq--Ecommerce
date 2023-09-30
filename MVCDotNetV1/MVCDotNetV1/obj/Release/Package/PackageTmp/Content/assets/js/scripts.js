


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
    //anytaskinprogress = true;

    $.ajax({
        method: "POST",
        url: "https://localhost:44301/Home/Add",
        contentType: "application/json",
        dataType: "json",
        data: data,
        success: (success) => {
            console.log(success);
            //anytaskinprogress = false;
        },
        error: (error) => { console.log(error); }
    });
}

function loadCartFromServer() {
    $.ajax({
        method: "GET",
        url: "https://localhost:44301/Home/Get",
        dataType: "json",
        success: (success) => {
            console.log("local"+success.cartContent);

            if (success.cartContent != "null" && success.isValid == true) {
                localStorage.setItem('shoppingCart', success.cartContent);
                cart =JSON.parse( success.cartContent);
            }
        },
        error: (error) => { console.log(error); }
    });
}

//this.onbeforeunload = function (event) {

//    saveCartOnServer(localStorage.getItem("shoppingCart"));
//    this.event.preventDefault();
//    //this.event.returnValue
//    this.event.returnValue = false;
//}
//let anytaskinprogress = false;


//this.onbeforeunload = exitfun;

//let tempWindow = open(location.href, "", "width=150, height=150");

//onclose = (e) => {
//    saveCartOnServer(localStorage.getItem("shoppingCart"));
//}

//function exitfun(event) {
//    //let tempWindow = open(location.href, "x", "width=150, height=150");
//    //tempWindow.focus();

//    saveCartOnServer(localStorage.getItem("shoppingCart"));

//    //while (anytaskinprogress) {
//    //    //return "are u sure to close?";
//    //}
//    console.log(event)

//    //return "Are you sure";
//}


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
    //cart = JSON.parse(localStorage.getItem('shoppingCart'));
 
    loadCartFromServer();
    displayCart();
}

var shoppingCart = (function shoppingCartFun() {
    // =============================
    // Private methods and propeties
    // =============================
    cart = [];

    // Constructor
/*function Item(name, price, count, AvailableQuantity) {*/
    function Item(name, price, count) {
        this.name = name;
        this.price = price;
        this.count = count;
    //    this.AvailableQuantity = AvailableQuantity
    }
    
   
    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
/*obj.addItemToCart = function (name, price, count, AvailableQuantity) {*/
    obj.addItemToCart = function (name, price, count) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart[item].count++;
                saveCart();
                return;
            }
        }
    /* var item = new Item(name, price, count, AvailableQuantity);*/
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
            console.log(i);
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

    if (isauthorize) {
        loadCart();

    }


    // Add item
    $('.add-to-cart').click(function (event) {
        event.preventDefault();
        var name = $(this).data('name');
        var price = Number($(this).data('price'));
      /*  var AvailableQuantity = Array($(this).data('AvailableQuantity'));*/

        if (isauthorize) {
        /*  shoppingCart.addItemToCart(name, price, 1, AvailableQuantity);*/
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
    console.log(cartArray);
    var output = "";
    for (var i in cartArray) {
        //var row = document.createElement("tr");
        //var name = document.createElement("td");
        //name.innerText = cartArray[i].name;
        //row.appendChild(name);

        //var quantity = document.createElement("SELECT");
        //quantity .setAttribute("id", "mySelect");


       





        //for (var x = 1; x <= cartArray[i].AvailableQuantity; x++) {

        //    var option = document.createElement("option");
        //    option.setAttribute("value", x);
        //    var t = document.createTextNode(x);
        //    option.appendChild(t);

        //    quantity .appendChild(option);
        //}


        //row.appendChild(quantity);


       
        output += "<tr>"
            + "<td>" + cartArray[i].name + "</td>"
            + "<td>(" + cartArray[i].price + ")</td>"
            
            //+ "<td><select>"
            //      for (var o in cartArray[i].AvailableQuantity) {
                    
            //          "<option value=1>" + o + "</option>"
            //          console.log(o);

            //      }
            //"</select ></td >"
                 + "<td><div class='input-group'><button class='minus-item input-group-addon btn btn-primary' data-name=" + cartArray[i].name + ">-</button>"
                 + "<input type='number' class='item-count form-control' data-name='" + cartArray[i].name + "' value='" + cartArray[i].count + "'>"
            + "<button class='plus-item btn btn-primary input-group-addon' data-name=" + cartArray[i].name + ">+</button></div></td>"


            //+ "<td><div class='dropdown'><button class='btn btn-secondary dropdown-toggle' type='button' id='dropdownMenuButton' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" + cartArray[i].name + "</button>< div class='dropdown-menu' aria-labelledby='dropdownMenuButton' > < button class='dropdown-item' href='#' data-name="+cartArray[i].price+" > </button > </div ></div ></td > "
  
            //    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
            //        <a class="dropdown-item" href="#">Action</a>
            //        <a class="dropdown-item" href="#">Another action</a>
            //        <a class="dropdown-item" href="#">Something else here</a>
            //    </div>
            //</div>
           

                + "<td><button class='delete-item btn btn-danger' data-name=" + cartArray[i].name + ">X</button></td>"
            + "<td> = </td>"
            + "<td>" + cartArray[i].total + "</td>"
            + "</tr>";
       
    }
/*$('.show-cart').html(row);*/
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

/*displayCart();*/
