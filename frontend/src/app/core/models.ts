/* ===== Auth DTOs ===== */
export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  name: string;
  email: string;
  password: string;
  phone?: string;
  address?: string;
}

export interface AuthResponseDto {
  token: string;
  name: string;
  email: string;
  role: string;
  expiresAt: string;
  isSuccess: boolean;
  message?: string;
}

/* ===== Product DTOs ===== */
export interface ProductResponseDto {
  id: number;
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  stockQuantity: number;
  isAvailable: boolean;
  isLowStock: boolean;
  categoryId: number;
  categoryName: string;
}

export interface CreateProductDto {
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  stockQuantity: number;
  lowStockThreshold: number;
  categoryId: number;
}

export interface UpdateProductDto {
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  stockQuantity: number;
  lowStockThreshold: number;
  categoryId: number;
}

/* ===== Order DTOs ===== */
export interface OrderItemDto {
  productId: number;
  quantity: number;
  unitPrice: number;
  lineTotal: number;
}

export interface OrderDto {
  id: number;
  customerName: string;
  customerPhone: string;
  customerAddress: string;
  totalAmount: number;
  status: number; // OrderStatus enum (0=Pending,1=Completed,2=Cancelled)
  notes?: string;
  placedAt: string;
  orderItems: OrderItemDto[];
}

export interface PlaceOrderDto {
  customerName: string;
  customerPhone: string;
  customerAddress: string;
  notes?: string;
}

/* ===== Cart DTOs (local + backend) ===== */
export interface CartItemLocal {
  product: ProductResponseDto;
  quantity: number;
}
