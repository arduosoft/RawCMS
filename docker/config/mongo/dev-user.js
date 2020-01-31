var db = new Mongo().getDB("admin");
db.auth("root", "password");

db.createUser({
  user: "dev",
  pwd: "password",
  roles: [
    {
      role: "readWrite",
      db: "rawcms"
    },
    {
      role: "dbAdmin",
      db: "rawcms"
    }
  ]
});
