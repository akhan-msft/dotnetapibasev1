
CREATE TABLE customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_name NVARCHAR(100) NOT NULL,
    customer_street_address NVARCHAR(255) NOT NULL,
    city NVARCHAR(100) NOT NULL,
    state NVARCHAR(100) NOT NULL,
    postal_code NVARCHAR(20) NOT NULL,
    create_date DATETIME DEFAULT GETDATE()
);

-- Create Orders Table
CREATE TABLE orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_details NVARCHAR(MAX) NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    order_total_amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

select * from customers;