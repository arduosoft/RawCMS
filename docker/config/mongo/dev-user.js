db.auth("root", "password");

db = db.getSiblingDB("rawcms");

db.createUser({
  user: "dev",
  pwd: "password",
  roles: [
    {
      role: "readWrite",
      db: "rawcms"
    },
    {
      role: "dbOwner",
      db: "rawcms"
    }
  ]
});
