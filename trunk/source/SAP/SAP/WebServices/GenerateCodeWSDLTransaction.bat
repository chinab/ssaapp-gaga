cd C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin
Set My_Path=%~dp0	
wsdl /l:CS /o:"%My_Path%\Transaction.cs" http://localhost/SBOWS/transaction.asmx?wsdl