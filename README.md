# RawCMS
RawCMS is an headless CMS written in asp.net core build for devopers that embrace API first tecnology.


# ROADMAP

## PHASE 1 - POC

- [x] Dynamic entity managment
- [x] Expose with regular web api services and swagger
- [ ] Expose with graphQL
- [ ] Expose with Odata
- [ ] Test and client sample

## PHASE 2 - Be ready for production
- [x] Lambda
- [x] Schema definition and validation
- [ ] Authentication and permission
- [ ] Data filtering per user
- [x] Lambda Http
- [ ] Plugin system

## PHASE 3 - Ready to send rocket on Mars
- [ ] design UI for schema managment
- [ ] design UI for data managment


# Project Structure

## RawCMS
It' s the front application. It expose services and documentation. This project contains all "presentation" logic:
- Model about payloads (web api, crud)


# LIMITS
- CRUD controller must manage exceptions and errors
- filter doesnt work. They will work only on main field (equal)
- Update is in "replace mode". No upsert or incremental save are allowed
- No validation on data yet implemented
- No UI to manange entities
- Insert on new collection produces collection creations 
- No persmission controls or authorization system
- No data filtering basing on user