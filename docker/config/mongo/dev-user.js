db.auth("root", "password");

db = db.getSiblingDB("admin");

db.createUser({
  user: "dev",
  pwd: "password",
  roles: [
    {
      role: "readWrite",
      db: "rawcms"
    }
  ]
});
