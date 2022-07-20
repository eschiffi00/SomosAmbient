<%@ WebHandler Language="VB" Class="Search_VB" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text

Public Class Search_VB : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim prefixText As String = context.Request.QueryString("q")
        Dim conn As SqlConnection = New SqlConnection
        conn.ConnectionString = ConfigurationManager _
                .ConnectionStrings("constr").ConnectionString
        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandText = ("select ContactName from Customers where " & _
                           "ContactName like @SearchText + '%'")
        cmd.Parameters.AddWithValue("@SearchText", prefixText)
        cmd.Connection = conn
        Dim sb As StringBuilder = New StringBuilder
        conn.Open()
        Dim sdr As SqlDataReader = cmd.ExecuteReader
        While sdr.Read
            sb.Append(sdr("ContactName")) _
                .Append(Environment.NewLine)
        End While
        conn.Close()
        context.Response.Write(sb.ToString)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class