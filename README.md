# Blogging Platform API

## Overview

This project is a simple blogging platform API built using ASP.NET Core Web API and SQLite. It provides basic CRUD operations for blog posts, search functionality, and pagination.

## Features

*   **CRUD Operations**: Create, Read, Update, Delete blog posts
*   **Search**: Search posts by term in title, content, and category
*   **Pagination**: Paginate list of posts

## Endpoints

*   `POST /posts`: Create a new post
*   `GET /posts`: Get a list of posts with pagination
*   `GET /posts/{id}`: Get a post by ID
*   `PUT /posts/{id}`: Update a post
*   `DELETE /posts/{id}`: Delete a post

## Requirements

*   .NET Core 3.1 or later
*   SQLite

## Usage

To run the project, clone the repository, and execute the following commands:

```bash
dotnet restore
dotnet run
```