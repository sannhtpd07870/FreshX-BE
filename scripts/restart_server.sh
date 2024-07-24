#!/bin/bash
# Dừng ứng dụng đang chạy
sudo systemctl stop kestrel-yourapp.service
# Sao chép mã nguồn mới được deploy
cp -r /var/www/your-app/output/* /var/www/API/
# Khởi động lại ứng dụng
sudo systemctl start kestrel-yourapp.service
