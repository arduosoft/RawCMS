## RawCms command line tool.

verbs:

login
insert
delete
replace
patch
list

Each verbs has some options:
...


Sample command:

List some collection data 
list -c uno -v -p

with pagination:
list -c uno -v -p -n 3 -s 10


Get configuration for current user (token)
login -u bob -p XYZ -i raw.client -t raw.secret -s http://localhost:49439

Insert data from file
insert -c test -f c:\temp\test.json 

Insert data from folder
insert -c test -d datalocal -r -p -v
