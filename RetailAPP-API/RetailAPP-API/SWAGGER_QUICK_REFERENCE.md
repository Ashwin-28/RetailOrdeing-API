# 📋 SWAGGER QUICK REFERENCE CARD

## 🚀 Start Here

```
1. Run:     dotnet run
2. Visit:   https://localhost:5001/
3. Test:    Try any endpoint with "Try it out"
4. Done!    ✅
```

---

## 🎯 Most Common Tasks

### Task 1: Get a JWT Token
```
1. Find:    POST /api/auth/login
2. Click:   "Try it out"
3. Enter:   {
              "email": "user@example.com",
              "password": "Password123"
            }
4. Click:   "Execute"
5. Copy:    The "accessToken" value
```

### Task 2: Add Token to All Requests
```
1. Click:   Green "Authorize" button (top-right)
2. Paste:   Bearer eyJhbGci...
3. Click:   "Authorize"
4. Click:   "Close"
Result:     Token automatically added to all requests!
```

### Task 3: Test a Protected Endpoint
```
1. Must:    Complete Task 2 first
2. Find:    GET /api/orders
3. Click:   "Try it out"
4. Click:   "Execute"
5. See:     Your orders displayed!
```

### Task 4: See Response Format
```
1. Click:   Any endpoint
2. Look:    "Responses" section
3. Check:   Status codes (200, 400, 401, etc.)
4. See:     Example response body
5. Learn:   What to expect
```

---

## 🔑 Important URLs

```
https://localhost:5001/                    Main Swagger UI
https://localhost:5001/swagger/v1/swagger.json    OpenAPI JSON Spec
https://localhost:5001/api/auth/login      Login Endpoint
https://localhost:5001/api/orders          Orders Endpoint
```

---

## 🎛️ UI Elements

```
┌─────────────────────────────────────────────────┐
│ Swagger UI - RetailApp API      [Authorize]    │
├─────────────────────────────────────────────────┤
│                                                 │
│ [Authorize]  ← Click to add JWT token          │
│                                                 │
│ ▼ auth       ← Endpoint groups (expandable)    │
│   ├─ POST /api/auth/login                      │
│   │  ├─ [Try it out] ← Click to test           │
│   │  ├─ Parameters section ← Request format    │
│   │  └─ Responses section ← Response format    │
│   │                                            │
│   └─ ...more endpoints                         │
│                                                 │
│ Models ← Data structures (at bottom)           │
│   ├─ AuthResponse                              │
│   ├─ LoginRequest                              │
│   └─ ...more models                            │
│                                                 │
└─────────────────────────────────────────────────┘
```

---

## 📌 Endpoints Overview

### Authentication
```
POST   /api/auth/register          [No token needed]
POST   /api/auth/login             [No token needed]
POST   /api/auth/change-password   [Token required]
GET    /api/auth/me                [Token required]
```

### Orders
```
GET    /api/orders                 [Token required]
GET    /api/orders/{id}            [Token required]
GET    /api/orders/all             [Token required]
PUT    /api/orders/{id}/status     [Token required]
```

---

## 💾 Data Structures

### Login Request
```json
{
  "email": "user@example.com",
  "password": "Password123"
}
```

### Login Response (Success)
```json
{
  "isSuccess": true,
  "message": "Login successful.",
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "email": "user@example.com",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Login Response (Failure)
```json
{
  "isSuccess": false,
  "message": "Invalid email or password."
}
```

### Order Object
```json
{
  "id": 1,
  "customerName": "John Doe",
  "customerPhone": "+1234567890",
  "customerAddress": "123 Main St",
  "totalAmount": 99.99,
  "status": "Pending",
  "placedAt": "2025-04-27T10:30:00Z",
  "orderItems": [...]
}
```

---

## 🚨 Response Codes Cheat Sheet

```
200 OK                  ✅ Request successful
201 Created             ✅ Resource created
400 Bad Request         ❌ Check your input
401 Unauthorized        ❌ Add JWT token
404 Not Found          ❌ Resource doesn't exist
500 Server Error       ❌ Contact support
```

---

## 🔐 JWT Token Explained

### What Is It?
A token that proves you're logged in. Valid for 60 minutes.

### How to Get It?
```
1. Login via POST /api/auth/login
2. Receive token in response
3. Use for all protected endpoints
```

### How to Use It?
```
Method 1: Authorize button (Recommended)
- Click green Authorize button
- Paste token with "Bearer " prefix
- All requests automatic

Method 2: Manual (for other tools)
- Add header: Authorization: Bearer YOUR_TOKEN
- Include in every request
```

### When Does It Expire?
Default: 60 minutes after login. Get a new one if you get 401 errors.

---

## ⚡ Common Workflows

### Workflow 1: Test Everything
```
Step 1: Open Swagger UI (https://localhost:5001/)
Step 2: Register a user (POST /api/auth/register)
Step 3: Login (POST /api/auth/login)
Step 4: Copy token from response
Step 5: Click Authorize, paste token
Step 6: Test protected endpoints
Step 7: Change password (POST /api/auth/change-password)
Step 8: Get current user (GET /api/auth/me)
Step 9: Test orders endpoints
Step 10: Done! ✅
```

### Workflow 2: Quick Test
```
Step 1: Click POST /api/auth/login
Step 2: Enter credentials
Step 3: Execute
Step 4: Copy token
Step 5: Authorize in Swagger
Step 6: Test any endpoint
```

### Workflow 3: Share with Team
```
Step 1: Send this URL: https://localhost:5001/
Step 2: Or send OpenAPI spec: /swagger/v1/swagger.json
Step 3: Team can test endpoints
Step 4: Done! ✅
```

---

## 🎓 Learning Tips

### Tip 1: Read Descriptions
Each endpoint has a description explaining what it does. Click to expand.

### Tip 2: Check Models
See data structures under "Models" section at the bottom.

### Tip 3: Try Different Inputs
Test with valid and invalid data to see error messages.

### Tip 4: Compare Responses
See how different status codes look (success vs error).

### Tip 5: Read Status Codes
Each response has status codes. Learn what they mean.

---

## 🆘 Troubleshooting

| Problem | Solution |
|---------|----------|
| Swagger not loading | Wait for `dotnet run` to finish |
| 401 on protected endpoint | Click Authorize, add token |
| Token not working | Get new token, re-authorize |
| Can't find endpoint | Use Ctrl+F to search |
| CORS error | Check browser console |
| Server error (500) | Check application logs |

---

## 📚 Where to Learn More

| Topic | File |
|-------|------|
| Quick start | SWAGGER_QUICK_START.md |
| Complete guide | SWAGGER_SETUP_GUIDE.md |
| What changed | SWAGGER_CHANGES_SUMMARY.md |
| Architecture | SWAGGER_ARCHITECTURE_DIAGRAM.md |
| Full details | README_SWAGGER_COMPLETE.md |
| Code changes | MODIFIED_FILES_LOG.md |

---

## 🎯 Your Goals

- [ ] Run `dotnet run`
- [ ] Visit `https://localhost:5001/`
- [ ] Test login endpoint
- [ ] Get JWT token
- [ ] Authorize in Swagger
- [ ] Test protected endpoint
- [ ] Success! 🎉

---

## ✨ Key Takeaways

✅ Professional API documentation available at root path
✅ "Try it out" button to test any endpoint
✅ Authorize button for JWT token management
✅ All requests include token automatically
✅ Response examples and error codes shown
✅ No external tools needed for testing
✅ Everything in one place
✅ Ready to use immediately

---

## 🚀 Now Go!

```
1. Open terminal
2. Run: dotnet run
3. Open browser: https://localhost:5001/
4. Start testing!
5. Enjoy your professional API docs! 🎊
```

---

**Print this page for quick reference!**
**Save the URL: https://localhost:5001/**
**Share with your team!**

---

*For questions, check the documentation files in your project.*
