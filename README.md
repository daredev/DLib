# DLib

The idea of this project is to provide automatic basic CRUD functionalities on SQL standarized lookup/dictionary tables.
There is also a MVC web project inside the solution for testing the library itself.

# SQL assumptions

The lookup table has the following structure: `(int ID = key auto increment NOT NULL, nvarchar value, bit is_active, date created_on, nvarchar created_by, date modified_on, nvarchar modified_by)`

# .NET assumptions

The controler used for handling should derive from `GenericLookupControler<TEntity>`, where `TEntity` is the class generated by EDMX model using Database First convention, the `TEntity` should derive from `GenericLookupModel` class using partial classes

# Requirements

All of the required references should be already included in packages.conf and download automaticaly while building the project
