# Blog backend API- Project Overview

## Project Goal

Create a backend for Blog application:

-Support full CRUD operations
-All users to create account and login
-Deploy to Azure
-Uses SCRUM workflow concepts
Introduction Azure DevOps practices

## Stack

-Back end will be in .net 9, asp.net core, ef core, sql server.
-Front end will be done in next.js with type script (to be done by jacob) flowbite(tailwind). Deploy TBA (Vercel or Azure).

## Application features
-Create account
-login
Delete Account

## Blog Features
-view all publish blog posts
-filter blog posts
-create blog posts
-edit blog posts
-delete blog posts
-unpublish blog posts
-publish blog posts

## Front end/ pages
-Create account page
-Blog view posts page of publish items
-Dashboard page ( This is to edit, delete, and publish and unpublish posts)

-**Blog pages**
-Display all publish blog items

-**DashBoard**
-User profile page
-Create blog posts
-edit blog posts
-delete blog posts

## project folder structure

## Controllers

### User Controller

Handle all the users interaction.

End Points:
-login
-add user
-update users
-delete users

#### Blog Controller

Handle all blog posts.

End points:

-Create Blog Item
-Get all blog items
-Get blog items from Category
-Get blog items by tags
-get blog item by date
-get publish blog items
-Update blog items
-Delete blog item
-get blog items by UserId

Delete will use for soft delete/ Publish Logic

----

## Models


## User Model
'''Csharp

int Id
string Username
string salt
string hash

### BlogItemModel

int Id
int UserId
string PublishName
string Title
string Image
string Description
string Date
string Category
Bool isPublished
Bool IsDeleted

## Item saved to our DB
### We need a way  to sign  in with  our user name and password

'''Csharp

### LoginModel
 
 string Username
 sting Password

 ### CreateAccountModel
int Id = 0
string Username
string Password

### PasswordModel

string salt

string hash

,,,

### Service
Context/Folder
-DataContext
-UserSerivce/file
-GetUserbyUsername
-login
-adduser
-updateuser
-deleteuser
### BlogItemService
-addblogitems
-getblogitems
-getblogitemsbycatory
-getblogitembytags
-getblogitemsbydate
-getblogitemsbyitems
-updateblogItems
-deleteblogItems
-getuserbyid

### Password Service

-Hash password
-Very Harsh Password