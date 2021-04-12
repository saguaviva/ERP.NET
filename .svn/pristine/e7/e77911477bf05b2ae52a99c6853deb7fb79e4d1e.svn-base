Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class clsEstadisticas
    Inherits clsADO

#Region "VARIABLES"

    Public dtEstadisticas As New DATATABLE
    Public dvEstadisticas As New DataView

    Private dtClientes As New DataTable
    Private dtProveedores As New DataTable
    Public aCamposAMostrar As New ArrayList
    Public aEmpresas As New ArrayList

    Public aTejidos As New ArrayList
    Public aHiloas As New ArrayList
    Public aTipos As New ArrayList
    Public aDocumentos As New ArrayList
    Public DesdeFecha As Date
    Public AFecha As Date
    Public aTiposDeArticulos As New ArrayList
    Public agruparFactures As Boolean = False
    Public agruparArticulos As Boolean = False
    Public agruparTipo As Boolean = False
    Public agruparClientes As Boolean = False
    Public agruparMeses As Boolean = False
    Public agruparAños As Boolean = False

    Public cTabla, detalleTabla, clienteProve, muestraArticulo As String


#End Region

    Private Sub CargaTablas()
        Try
            CargaTabla(tablaClientes, "CODI", "NOM", dtClientes, True)
            Dim dr As DataRow = dtClientes.NewRow : dr("CODI") = 0 : dr("NOM") = rm.GetString("TODOSLOSCLIENTES")
            dtClientes.Rows.Add(dr)

            CargaTabla(tablaProveedores, "CODI", "NOM", dtProveedores, True)
            dr = dtClientes.NewRow : dr("CODI") = 0 : dr("NOM") = rm.GetString("TODOSLOSPROVEEDORES")
            dtClientes.Rows.Add(dr)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Sub New()
        Try
            CargaTablas()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Function PonerTipoDocumento() As String
        Dim i As Integer
        Dim ret As String
        Try
            ret = " (" & cTabla & ".DOCUMENT = """ & aDocumentos(0) & """ "
            For i = 1 To aDocumentos.Count - 1
                ret = " OR " & cTabla & ".DOCUMENT = """ & aDocumentos(i) & """ "
            Next
            Return ret & ") "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function PonerEmpresa() As String
        Dim i As Integer
        Dim ret As String
        Try
            ret = " (" & cTabla & ".CENTRO = """ & aEmpresas(0) & """ "
            For i = 1 To aEmpresas.Count - 1
                ret = " OR " & cTabla & ".CENTRO = """ & aEmpresas(i) & """"
            Next
            Return ret & ") "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function PonerTejidos() As String
        Dim i As Integer
        Dim ret As String
        Try
            Return " (0 = 0) "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function PonerIntervaloFechas() As String
        Dim i As Integer
        Dim ret As String
        Try
            ret = " (" & cTabla & ".DATA >= """ & ConvertirAfechaMysql(DesdeFecha) & """ AND " & cTabla & ".DATA <= """ & ConvertirAfechaMysql(AFecha) & """) "
            Return ret

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function PonerTipoArticulo()
        Dim i As Integer
        Dim ret As String = ""
        Try
            If aTiposDeArticulos.Count <> 0 Then ret = "("
            For i = 0 To aTiposDeArticulos.Count - 1
                If ret <> "(" Then ret = ret & " OR "
                ret = ret & " " & detalleTabla & ".TIPUS = """ & aTiposDeArticulos(i) & """ "
            Next
            Return ret & ")"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Public Function GenerarWhere() As String
        Dim str As String
        Try
            'PonerTejidos() & """ AND """ &
            str = str & PonerEmpresa() & " AND " & PonerTipoDocumento() & " AND " & PonerIntervaloFechas() & " AND " & PonerTipoArticulo()
            Return str

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Sub CrearTablaEst()
        Dim i As Integer
        Dim dc As DataColumn
        Try
            '!!!!
            Try
                aCamposAMostrar.Clear()
                aCamposAMostrar.Add("FRA")
                aCamposAMostrar.Add("TOTALSENSEIVA")
                aCamposAMostrar.Add("TOTALCONVIA")
                aCamposAMostrar.Add("NOM")
                aCamposAMostrar.Add("CLIENT")

            Catch ex As Exception
            End Try

            'dtEstadisticas.Clear()
            'dtEstadisticas.Columns.Clear()
            'For i = 0 To aCamposAMostrar.Count - 1

            '    If aCamposAMostrar(i) = "FRA" OrElse aCamposAMostrar(i) = "TOTALCONIVA" Then
            '        dc = New DataColumn(aCamposAMostrar(i), GetType(Decimal))
            '    Else
            '        dc = New DataColumn(aCamposAMostrar(i))
            '    End If
            '    dtEstadisticas.Columns.Add(dc)
            'Next

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Function GenerarGroup() As String
        Dim ret As String = ""
        Try
            If agruparMeses Then
                If ret = "" Then
                    ret = " GROUP BY MES "
                Else
                    ret = ret & ", MES "
                End If
            End If
            If agruparAños Then
                If ret = "" Then
                    ret = " GROUP BY ANY "
                Else
                    ret = ret & ", ANY "
                End If
            End If
            If agruparFactures Then
                If ret = "" Then
                    ret = " GROUP BY " & detalleTabla & ".FRA "
                Else

                End If
            End If
            If agruparArticulos Then
                If ret = "" Then
                    ret = " GROUP BY " & detalleTabla & "." & muestraArticulo & " "
                Else
                    ret = ret & ", " & detalleTabla & "." & muestraArticulo & " "
                End If
            End If
            If agruparClientes Then
                If ret = "" Then
                    ret = " GROUP BY " & cTabla & "." & clienteProve & " "
                Else
                    ret = ret & ", " & cTabla & "." & clienteProve & " "
                End If
            End If
            If agruparTipo Then
                If ret = "" Then
                    ret = " GROUP BY " & detalleTabla & ".TIPUS "
                Else
                    ret = ret & ", " & detalleTabla & ".TIPUS "
                End If
            End If
            If aTiposDeArticulos.Count = 1 Then
                'Return " GROUP BY " & detalleTabla & ".FRA, " & detalleTabla & ".TIPUS "

            Else
                'Return " GROUP BY " & detalleTabla & ".FRA "
            End If

            Return ret

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Public Sub GenerarEstProve()
        Dim strWhere As String
        Dim strSQL As String
        Dim dar As MySql.Data.MySqlClient.MySqlDataReader
        Dim cmd As MySql.Data.MySqlClient.MySqlCommand
        Dim dr As DataRow
        Try
            cTabla = "CACTUR"
            detalleTabla = "DCACTU"
            clienteProve = "PROVE"
            muestraArticulo = "ARTICLE"

            If Not agruparFactures Then
                strSQL = "SELECT MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY, " & cTabla & ".FRA, " & _
                    " " & cTabla & ".DATA, " & _
                    " " & cTabla & ".PROVE, " & _
                    " PROVE.NOM AS NOMPROVE, " & _
                    " IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".NLINEA, " & _
                    " " & detalleTabla & ".UNITATS, " & _
                    " " & detalleTabla & ".IMPORT AS IMPORTSINIVA " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
                '" ROUND(" & detalleTabla & ".IMPORT * (1+IVA.IVA/100), 2) AS IMPORTIVA " & _
            Else
                strSQL = "SELECT  MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY," & cTabla & ".FRA, " & _
                    " " & cTabla & ".DATA, " & _
                    " " & cTabla & ".PROVE, " & _
                    " PROVE.NOM AS NOMCLIENT, " & _
                    " IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".NLINEA, " & _
                    " " & detalleTabla & ".UNITATS, " & _
                    " " & detalleTabla & ".IMPORT AS IMPORTSINIVA " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
                '" SUM(ROUND(" & detalleTabla & ".IMPORT * (1 + IVA.IVA/100), 2)) AS IMPORTIVA " & _
                '" ROUND(" & detalleTabla & ".IMPORT * (1+IVA.IVA/100), 2) AS IMPORTIVA " & _
            End If

            If agruparArticulos Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " SUM(ROUND(" & detalleTabla & ".UNITATS,2)), " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
            End If
            If agruparClientes Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " PROVE.NOM AS NOMCLIENT, " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
            End If
            If agruparTipo Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
            End If
            If agruparMeses Then
                strSQL = "SELECT   MONTH(" & detalleTabla & ".DATA) AS MES, IVA.IVA, " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " FROM CACTUR " & _
                    " LEFT JOIN PROVE ON (PROVE.CODI = " & cTabla & ".PROVE) " & _
                    " LEFT JOIN DCACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "
            End If

            strWhere = " WHERE " & GenerarWhere() & GenerarGroup()
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strSQL & strWhere, cnn)
            ACN()
            'dar = cmd.ExecuteReader

            'CrearTablaEst()
            dtEstadisticas = New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'dtEstadisticas.SelectCommand = cmd
            da.Fill(dtEstadisticas)
            'dtEstadisticas.Constraints.Clear()
            'dtEstadisticas.Fill()
            CCN()
            dvEstadisticas = dtEstadisticas.DefaultView

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Sub GenerarEst()
        Dim strWhere As String
        Dim strSQL As String
        Dim dar As MySql.Data.MySqlClient.MySqlDataReader
        Dim cmd As MySql.Data.MySqlClient.MySqlCommand
        Dim dr As DataRow
        Try
            cTabla = "FACTUR"
            detalleTabla = "DFACTU"
            clienteProve = "CLIENT"
            muestraArticulo = "MOSTRA"

            If Not agruparFactures Then
                strSQL = "SELECT  MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY," & cTabla & ".FRA, " & _
                    " " & cTabla & ".DATA, " & _
                    " " & cTabla & "." & clienteProve & ", " & _
                    " CLIENTS.NOM AS NOMCLIENT, " & _
                    " IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".NLINEA, " & _
                    " " & detalleTabla & ".UNITATS, " & _
                    " " & detalleTabla & ".IMPORT AS IMPORTSINIVA " & _
                    " FROM FACTUR " & _
                    " LEFT JOIN CLIENTS ON (CLIENTS.CODI = " & cTabla & "." & clienteProve & ") " & _
                    " LEFT JOIN DFACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "
                '" ROUND(" & detalleTabla & ".IMPORT * (1+IVA.IVA/100), 2) AS IMPORTIVA " & _
            Else
                strSQL = "SELECT  MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY," & cTabla & ".FRA, " & _
                    " " & cTabla & ".DATA, " & _
                    " " & cTabla & "." & clienteProve & ", " & _
                    " CLIENTS.NOM AS NOMCLIENT, " & _
                    " IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".NLINEA, " & _
                    " " & detalleTabla & ".UNITATS, " & _
                    " " & detalleTabla & ".IMPORT AS IMPORTSINIVA " & _
                    " FROM FACTUR " & _
                    " LEFT JOIN CLIENTS ON (CLIENTS.CODI = " & cTabla & "." & clienteProve & ") " & _
                    " LEFT JOIN DFACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "
                '" SUM(ROUND(" & detalleTabla & ".IMPORT * (1 + IVA.IVA/100), 2)) AS IMPORTIVA " & _
                '" ROUND(" & detalleTabla & ".IMPORT * (1+IVA.IVA/100), 2) AS IMPORTIVA " & _
            End If

            If agruparArticulos Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " " & detalleTabla & "." & muestraArticulo & " AS ARTICLE, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " SUM(ROUND(" & detalleTabla & ".UNITATS,2)), " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM FACTUR " & _
                    " LEFT JOIN CLIENTS ON (CLIENTS.CODI = " & cTabla & "." & clienteProve & ") " & _
                    " LEFT JOIN DFACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "
            End If
            If agruparClientes Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " CLIENTS.NOM AS NOMCLIENT, " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM FACTUR " & _
                    " LEFT JOIN CLIENTS ON (CLIENTS.CODI = " & cTabla & "." & clienteProve & ") " & _
                    " LEFT JOIN DFACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "
            End If
            If agruparTipo Then
                strSQL = "SELECT   MONTH(" & cTabla & ".DATA) AS MES, YEAR(" & cTabla & ".DATA) AS ANY,IVA.IVA, " & _
                    " " & detalleTabla & ".TIPUS, " & _
                    " SUM(ROUND(" & detalleTabla & ".IMPORT,2)) AS IMPORTSINIVA " & _
                    " FROM FACTUR " & _
                    " LEFT JOIN CLIENTS ON (CLIENTS.CODI = " & cTabla & "." & clienteProve & ") " & _
                    " LEFT JOIN DFACTU ON (" & detalleTabla & ".FRA = " & cTabla & ".FRA) " & _
                    " LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "
            End If

            strWhere = " WHERE " & GenerarWhere() & GenerarGroup()
            cmd = New MySql.Data.MySqlClient.MySqlCommand(strSQL & strWhere, cnn)
            ACN()
            'dar = cmd.ExecuteReader

            'CrearTablaEst()
            dtEstadisticas = New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'dtEstadisticas.SelectCommand = cmd
            'dtEstadisticas.Constraints.Clear()
            da.Fill(dtEstadisticas)
            '            dtEstadisticas.Fill()
            CCN()
            dvEstadisticas = dtEstadisticas.DefaultView

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String

    End Function
    Friend Overrides Function genWhere() As String

    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String

    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer

    End Function

#End Region

End Class
