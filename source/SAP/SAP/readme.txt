1.Có lẽ khi edit trên lưới thì nên mở popup lên cho dễ xử lý, còn thao tác trên lưới với custom control như vậy rất mất thời gian.
2.Mô tả cách gọi Popup:
- PurchaseOrder.aspx đăng ký control nhận Callback: Main.myUpdatePanelId = '<%= myUpdatePanel.ClientID %>';
- Button đăng ký mở Popup với tham số: <a href="javascript:Main.openEditItem('param')">Edit</a>
- Hàm Main.openEditItem sẽ mở trang PurchaseOrder_EditItem.aspx với thông số đặt ở querystring
- PurchaseOrder_EditItem.aspx gọi OK event và gọi đến hàm close popup:  Main.okEditClick() và chỉ ra control nhận callback là Main.myUpdatePanelId
- Sự kiện OnLoadComplete của PurchaseOrder.aspx sẽ nhận callback và xử lý
Chú ý: lưu thông tin trong Session.
