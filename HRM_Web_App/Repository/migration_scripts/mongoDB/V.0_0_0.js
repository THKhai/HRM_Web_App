db.createUser({
    user:"khai_TR",
    pwd:"09122002",
    roles:[
        {
            role:"readWrite",
            db:"HRMWebApplication"
        }
    ]
})

use("HRMWebApplication");
db.createCollection("Users");