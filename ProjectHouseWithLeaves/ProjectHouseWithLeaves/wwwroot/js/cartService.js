// Cart Service for handling all cart operations
const cartService = {
    async getCart() {
        try {
            const response = await fetch(`https://localhost:7115/Cart/GetCartData/${window.userId}`);
            if (!response.ok) throw new Error('Failed to fetch cart');
            return await response.json();
        } catch (error) {
            console.error('Error fetching cart:', error);
            return [];
        }
    },

    async addItem(productId, quantity) {
        try {
            const response = await fetch('https://localhost:7115/Cart/AddItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ productId, quantity })
            });
            if (!response.ok) throw new Error('Failed to add item');
            return await response.json();
        } catch (error) {
            console.error('Error adding item:', error);
            return null;
        }
    },

    async removeItem(productId) {
        try {
            const response = await fetch('https://localhost:7115/Cart/RemoveItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ productId })
            });
            if (!response.ok) throw new Error('Failed to remove item');
            return await response.json();
        } catch (error) {
            console.error('Error removing item:', error);
            return null;
        }
    },

    async decreaseQuantity(productId, quantity) {
        try {
            const response = await fetch('https://localhost:7115/Cart/DeleteQuantityItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ productId, quantity })
            });
            if (!response.ok) throw new Error('Failed to decrease quantity');
            return await response.json();
        } catch (error) {
            console.error('Error decreasing quantity:', error);
            return null;
        }
    },

    async clearCart() {
        try {
            const response = await fetch('https://localhost:7115/Cart/ClearCart', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                }
            });
            if (!response.ok) throw new Error('Failed to clear cart');
            return await response.json();
        } catch (error) {
            console.error('Error clearing cart:', error);
            return null;
        }
    }
}; 