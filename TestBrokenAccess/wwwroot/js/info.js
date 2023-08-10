const userDiv = document.getElementById("user-info");

if (getCookie('email')) {
    const email = getCookie('email');
    fetch(`https://localhost:7096/api/ManageProfileApi?email=${email}`)

        // Converting received data to JSON
        .then(response => response.json())
        .then(user => {

            //var tableContainer = document.getElementById("userTableContainer");
            var table = document.createElement("table");
            table.className = "user-table";

            // Create table header row
            var headerRow = document.createElement("tr");
            headerRow.innerHTML = "<th>Name</th><th>Email</th><th>Role</th>";
            table.appendChild(headerRow);

            // Create rows for each user
            var userRow = document.createElement("tr");
            userRow.innerHTML = `<td>${user.Name}</td><td>${user.EmailAddress}</td><td>${user.Role == 0 ? "Admin" : "User"}</td>`;
            table.appendChild(userRow);

            // Display result
            userDiv.appendChild(table);
        });
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}