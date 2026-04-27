# Swagger Quick Start Guide

## 🎯 5-Minute Setup Complete!

Your API now has professional interactive documentation via **Swagger/Swashbuckle**.

---

## 🚀 Start Using It

### Step 1: Run Your API
```bash
dotnet run
```

Output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
      Now listening on: http://localhost:5000
```

### Step 2: Open Swagger UI
Click or navigate to:
```
https://localhost:5001
```

You'll see this beautiful interface:

```
╔═════════════════════════════════════════════════════════════════╗
║           Swagger UI - RetailApp API                  Authorize  ║
╠═════════════════════════════════════════════════════════════════╣
║                                                                 ║
║  ▼ auth                                                         ║
║    ├─ POST   /api/auth/register      Register a new user       ║
║    ├─ POST   /api/auth/login         Authenticate user         ║
║    ├─ POST   /api/auth/change-password   Change password       ║
║    └─ GET    /api/auth/me            Get current user info     ║
║                                                                 ║
║  ▼ orders                                                       ║
║    ├─ GET    /api/orders             Get user's orders         ║
║    ├─ GET    /api/orders/{id}        Get specific order        ║
║    ├─ GET    /api/orders/all         Get all orders            ║
║    └─ PUT    /api/orders/{id}/status Update order status       ║
║                                                                 ║
║  ▼ products                                                     ║
║    └─ ...                                                       ║
║                                                                 ║
╚═════════════════════════════════════════════════════════════════╝
```

---

## 🔐 Complete Testing Workflow

### Phase 1: Register a User

1. **Find the endpoint**: Click on `POST /api/auth/register`
2. **Expand it**: Endpoint expands showing details
3. **Click "Try it out"**: Blue button appears
4. **Edit request body**:
   ```json
   {
     "email": "testuser@example.com",
     "password": "TestPass123",
     "confirmPassword": "TestPass123",
     "phoneNumber": "+1234567890"
   }
   ```
5. **Click "Execute"**: Request is sent
6. **See response**: 
   ```json
   {
     "isSuccess": true,
     "message": "User registered successfully. Please log in.",
     "userId": "550e8400-e29b-41d4-a716-446655440000",
     "email": "testuser@example.com"
   }
   ```

---

### Phase 2: Login & Get Token

1. **Find endpoint**: `POST /api/auth/login`
2. **Click "Try it out"**
3. **Enter credentials**:
   ```json
   {
     "email": "testuser@example.com",
     "password": "TestPass123"
   }
   ```
4. **Click "Execute"**
5. **Copy the token**:
   ```
   "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
   ```

---

### Phase 3: Authorize Swagger

1. **Click green "Authorize" button** (top-right corner)
2. **See this dialog**:
   ```
   ╔════════════════════════════════════════════════╗
   ║ Available authorizations                      ║
   ║                                                ║
   ║ Bearer (HTTP Bearer)                           ║
   ║ [value: ________________] [Authorize] [Logout]║
   ║                                                ║
   ║ Example: Bearer YOUR_TOKEN_HERE                ║
   ╚════════════════════════════════════════════════╝
   ```
3. **Paste your token** (with or without "Bearer " prefix):
   ```
   Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
   ```
4. **Click "Authorize"**
5. **Click "Close"**

---

### Phase 4: Test Protected Endpoints

Now you can test any endpoint that requires authentication:

#### Get User's Orders
1. **Find**: `GET /api/orders`
2. **Click "Try it out"**
3. **Click "Execute"**
4. **See response**:
   ```json
   [
     {
       "id": 1,
       "customerName": "John Doe",
       "totalAmount": 99.99,
       "status": "Pending"
     }
   ]
   ```

#### Get Specific Order
1. **Find**: `GET /api/orders/{id}`
2. **Click "Try it out"**
3. **Enter ID**: `1`
4. **Click "Execute"**
5. **See order details**

#### Update Order Status
1. **Find**: `PUT /api/orders/{id}/status`
2. **Click "Try it out"**
3. **Enter ID**: `1`
4. **Enter body**:
   ```json
   {
     "status": "Shipped"
   }
   ```
5. **Click "Execute"**
6. **See confirmation**:
   ```json
   {
     "message": "Order status updated successfully."
   }
   ```

---

## 📊 Understanding the Response Codes

| Code | Meaning | Action |
|------|---------|--------|
| **200** | ✅ Success | Your request worked |
| **201** | ✅ Created | Resource was created |
| **400** | ❌ Bad Request | Check your input |
| **401** | ❌ Unauthorized | Add JWT token via Authorize |
| **404** | ❌ Not Found | Resource doesn't exist |
| **500** | ❌ Server Error | Contact support |

---

## 🔑 JWT Token Explained

Your JWT token contains three parts:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiI1NTBlODQwMC1lMjliLTQxZDQtYTcxNi00NDY2NTU0NDAwMDAiLCJlbWFpbCI6ImFkbWluQGV4YW1wbGUuY29tIn0.
5eFu6_OlXA3XFHlDnF3yd_x7jQ_0c5b1rW8F5kJ6cZo
│────────────────────────────────────────│ │──────────────────────────────────────│ │────────────────────────────────────│
              Header                               Payload (your info)                    Signature (security)
```

**Header**: Specifies algorithm (HS256)
**Payload**: Contains your user info (id, email, issued time, expiration)
**Signature**: Ensures token isn't tampered with

**Token Expires**: 60 minutes (configurable in appsettings.json)

---

## 🛠️ Common Tasks

### Task: Test an Unauthenticated Endpoint
**Example**: `POST /api/auth/register`
1. Open endpoint
2. Click "Try it out"
3. No need to authorize (public endpoint)
4. Execute

### Task: Test an Authenticated Endpoint
**Example**: `GET /api/orders`
1. First: Authorize with JWT token
2. Open endpoint
3. Click "Try it out"
4. Execute

### Task: See Request/Response Details
1. Expand endpoint
2. Look for **"Request body"** section (request schema)
3. Look for **"Responses"** section (all possible responses)
4. Look for **"Models"** section at bottom (data structures)

### Task: Debug a Failed Request
1. Check **Response Status Code** (200, 400, 401, etc.)
2. Check **Response Body** (error message)
3. Look at **Response Headers** (token info)
4. Check your input in **Request Body**

---

## 📍 Important URLs

| Purpose | URL |
|---------|-----|
| **Swagger UI** | `https://localhost:5001/` |
| **OpenAPI JSON Spec** | `https://localhost:5001/swagger/v1/swagger.json` |
| **API Root** | `https://localhost:5001/api/` |
| **Auth Endpoints** | `https://localhost:5001/api/auth/...` |
| **Order Endpoints** | `https://localhost:5001/api/orders/...` |

---

## 🎓 Pro Tips

### Tip 1: Share API with Frontend Team
Send them this URL:
```
https://localhost:5001/swagger/v1/swagger.json
```
They can import it into their tools (Postman, Insomnia, etc.)

### Tip 2: Bookmark Swagger
Add to bookmarks for quick access during development

### Tip 3: Check Token Expiration
If Authorize stops working:
1. Login again to get a new token
2. Click Authorize again
3. Paste new token

### Tip 4: Use "Models" Tab
See all data structures at the bottom of Swagger UI under "Models" section

### Tip 5: Read Descriptions
Each endpoint has descriptions - click to expand for more details

---

## 🚨 Troubleshooting

### Problem: "Authorize" button not visible
**Solution**: Check that you have JWT endpoints. It should be green and visible at top-right.

### Problem: Getting 401 Unauthorized on protected endpoints
**Solution**: 
1. Make sure you clicked "Authorize" button
2. Paste token with "Bearer " prefix
3. Make sure token isn't expired

### Problem: Getting 400 Bad Request
**Solution**:
1. Check your JSON syntax (use proper quotes, commas)
2. Verify all required fields are included
3. Check field types (string vs number)

### Problem: API not accessible
**Solution**:
1. Make sure you ran `dotnet run`
2. Try `https://` instead of `http://`
3. Check port number (should be 5001 by default)

---

## 📚 Documentation

For detailed information, see:
- **SWAGGER_SETUP_GUIDE.md** - Complete setup guide
- **SWAGGER_CHANGES_SUMMARY.md** - What changed
- **AUTHENTICATION_GUIDE.md** - Auth details
- **AUTH_IMPLEMENTATION_SUMMARY.md** - Auth system overview

---

## ✨ What You Have Now

✅ **Professional API Documentation** - Swagger UI
✅ **Interactive Endpoint Testing** - "Try it out" feature
✅ **JWT Authentication** - Authorize button
✅ **Auto-Generated Docs** - From code comments
✅ **OpenAPI Spec** - Machine-readable API definition
✅ **Response Examples** - See exactly what to expect
✅ **Schema Validation** - Type checking
✅ **Error Documentation** - All error codes explained

---

## 🎯 You're Ready!

1. ✅ Run: `dotnet run`
2. ✅ Open: `https://localhost:5001/`
3. ✅ Test: Register → Login → Use Token → Test API
4. ✅ Done!

Happy testing! 🚀
