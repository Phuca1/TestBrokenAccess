// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function modifyRole(userId, role) {
    //fetch("/ManageUserProfile/UpdateRole", {
    //    method: "POST",
    //    body: JSON.stringify({
    //        userId,
    //        role
    //    })
    //}).catch(err => {
    //    console.log("Error" + err);
    //});
    $.post("/ManageUserProfile/UpdateRole", {
        userId,
        role
    });
}