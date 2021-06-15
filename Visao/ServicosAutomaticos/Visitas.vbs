Call RunIt()
Sub RunIt()

Dim RequestObj
Dim URL
Set RequestObj = CreateObject("Microsoft.XMLHTTP")

'Request URL...
URL = "http://ambientalismanager.com.br/ServicosAutomaticos/NotificacoesDeVisitas.aspx"

'Open request and pass the URL
RequestObj.open "POST", URL , false

'Send Request
RequestObj.Send

'cleanup
Set RequestObj = Nothing
End Sub