//Goal

//Create a Back End for a Blog Site
//Create a Front End for our Blog Site
//Deploy to Azure
//Learn about DevOps and SCRUM

Creat an API for Blog. This API must handel all CRUD functions
Create, Read, Update, Delete.

//In this app user should be able to log in. Create an account.
Blog page to view all published items
Dashboard(The user profile page for them to edit, delete, and add blog items)

We will talk about folder structure 

Controller//Folders
 UserController (This will handel all our user interactions)
    Login//endpoints
    Add a user//endpoints
    Update a User
    Delete a User
BlogController//file
    Add blog items // endpoint C
    GetAllBlogItems // endpoint R
    GetBlogItemsByCategory
    GetBlogItemsByTags
    GetBlogItemsByDate
    UpdateBlogItems // endpoint U
    DeleteBlogItems // endpoint D

Model // Folder
    UserModel
        int ID
        string Username
        string Salt
        string Hash 256 characters

    BlogItemModel
        int ID
        int UserID
        string PublisherName
        string Title
        string Image
        string Description
        string Date
        string Category
        bool IsPublished
        bool IsDeleted

-----------------Items that will be saved to out data base DB are above-----------------

LoginModel
    string Username
    string Password
CreateAccountModel
    int Id = 0
    string Username
    string Password
passwordModel
    string Salt
    string Hash

Services//Folder
    Context//Folder

    UserService//file
        GetUsersByUsername
        Login
        Add User
        Delete User
    BlogItemService//file
        Add blog items // functions C
        GetAllBlogItems // functions R
        GetBlogItemsByCategory
        GetBlogItemsByTags
        GetBlogItemsByDate
        UpdateBlogItems // functions U
        DeleteBlogItems // functions D
        GetUserById // functions

PasswordService//file
    Hash Password
    Verify Hash Password

 "_comment": "Server Admin log in: academyblogAdmin Password: AcademyBlogPassword!"