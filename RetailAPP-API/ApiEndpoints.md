# Retail App API Endpoints

This document outlines the standard REST API endpoints for the Retail Ordering Application based on the core features required: Admin actions, User actions, Products, Orders, and Cart.

## Authentication (Identity Framework)
These endpoints handle user registration and login using ASP.NET Core Identity.

| Method | Endpoint | Description | Request Body |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/register` | Register a new user or admin. | `{"email": "...", "password": "...", "role": "User"}` |
| `POST` | `/api/auth/login` | Login and receive an authentication response/token. | `{"email": "...", "password": "..."}` |

## Products (Admin & User)
Endpoints for managing and viewing the menu/products.

| Method | Endpoint | Description | Access |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/products` | Get a list of all products (with optional category filter). | Public / User |
| `GET` | `/api/products/{id}` | Get details of a specific product. | Public / User |
| `POST` | `/api/products` | Add a new product to the catalog. | **Admin** |
| `PUT` | `/api/products/{id}` | Update an existing product (e.g., change price/stock). | **Admin** |
| `DELETE` | `/api/products/{id}` | Delete a product from the catalog. | **Admin** |

## Categories
Endpoints for managing product categories (Pizza, Cold Drinks, Breads).

| Method | Endpoint | Description | Access |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/categories` | Get all available categories. | Public / User |
| `POST` | `/api/categories` | Add a new category. | **Admin** |

## Cart
Endpoints for users to manage their shopping cart before placing an order.

| Method | Endpoint | Description | Access |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/cart` | View current user's cart items. | User |
| `POST` | `/api/cart/add` | Add a product to the cart. | User |
| `DELETE` | `/api/cart/remove/{itemId}` | Remove a specific item from the cart. | User |
| `POST` | `/api/cart/checkout` | Process the cart and convert it into a confirmed Order. | User |

## Orders
Endpoints for order placement and viewing order history.

| Method | Endpoint | Description | Access |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/orders` | View the current user's order history. | User |
| `GET` | `/api/orders/{id}` | View details of a specific order. | User |
| `GET` | `/api/orders/all` | View all orders across the platform. | **Admin** |
| `PUT` | `/api/orders/{id}/status` | Update the status of an order (e.g., Pending -> Completed). | **Admin** |
