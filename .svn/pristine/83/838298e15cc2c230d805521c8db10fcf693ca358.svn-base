Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports System.Runtime.InteropServices
Imports System
Imports System.IO

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Collections


Public Class clsRemesa
    Inherits clsADO

#Region "VARIABLES"

    Friend dvVencimientos As New DataView
    Public lineasRemesa As clsLineasRemesa
    Public cuentasRemesa As clsCuentasRemesa
    Public dtBancs As New DataTable
    Public dtRemesor As New DataTable
    Public dtCuentas As New DataTable
    Public dtPor As New DataTable
    Public dtClients As New DataTable

#End Region

#Region "CAMPOS"

    '+-----------+---------------------+------+-----+---------+-------+
    '| Field     | Type                | Null | Key | Default | Extra |
    '+-----------+---------------------+------+-----+---------+-------+
    '| CODI      | int(11)             | NO   | PRI | 0       |       |
    '| COBRO     | tinyint(1) unsigned | NO   |     | 0       |       |
    '| OFICINA   | varchar(255)        | YES  |     | NULL    |       |
    '| REMESOR   | int(11)             | YES  |     | NULL    |       |
    '| FECHA     | date                | YES  |     | NULL    |       |
    '| TOTAL     | double(15,2)        | YES  |     | NULL    |       |
    '| BANC      | varchar(4)          | YES  |     | NULL    |       |
    '| CENTRO    | char(1)             | NO   | PRI | C       |       |
    '| LINEA     | varchar(255)        | YES  |     | NULL    |       |
    '| DESCUENTO | tinyint(1) unsigned | NO   |     | 1       |       |
    '+-----------+---------------------+------+-----+---------+-------+
    '10 rows

    Private mCODI As Integer
    Public Property CODI() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCODI = nzn(dvForm(PA).Row("CODI"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCODI, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CODI, 0) Then
                mCODI = nzn(Value, 0)
                dvForm(PA).Row("CODI") = nzn(mCODI, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOBRO As Boolean
    Public Property COBRO() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOBRO = dvForm(PA).Row("COBRO")
            Catch ex As Exception : End Try
            Return mCOBRO
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> COBRO Then
                mCOBRO = Value
                dvForm(PA).Row("COBRO") = mCOBRO : guardarDV()
            End If
        End Set
    End Property

    Private mOFICINA As String
    Public Property OFICINA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOFICINA = general.nz(dvForm(PA).Row("OFICINA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOFICINA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OFICINA, "") Then
                mOFICINA = general.nz(Value, "")
                dvForm(PA).Row("OFICINA") = general.nz(mOFICINA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREMESOR As Integer
    Public Property REMESOR() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mREMESOR = nzn(dvForm(PA).Row("REMESOR"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREMESOR, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtRemesor, CCRemesor, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mREMESOR = nzn(Value, 0)
                    dvForm(PA).Row("NOMREMESOR") = general.OBN(Value, dtRemesor, CNRemesor)
                    dvForm(PA).Row("REMESOR") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("REMESOR") = DBNull.Value

                    dvForm(PA).Row("NOMREMESOR") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("REMESOR") = DBNull.Value

                dvForm(PA).Row("NOMREMESOR") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEREMESOR"))
            End If
        End Set
    End Property

    Private mNOMREMESOR As String
    Public Property NOMREMESOR() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMREMESOR = general.nz(dvForm(PA).Row("NOMREMESOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMREMESOR, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMREMESOR = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMREMESOR") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMREMESOR") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFECHA As Date
    Public Property FECHA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mFECHA = dvForm(PA).Row("FECHA")
            Catch ex As Exception : End Try
            Return mFECHA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            If Value <> FECHA Then
                mFECHA = Value
                dvForm(PA).Row("FECHA") = mFECHA : guardarDV()
            End If
        End Set
    End Property

    Private mTOTAL As Double
    Public Property TOTAL() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTOTAL = nzn(dvForm(PA).Row("TOTAL"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTOTAL, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TOTAL, 0) Then
                mTOTAL = nzn(Value, 0)
                dvForm(PA).Row("TOTAL") = nzn(mTOTAL, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mBANC As String
    Public Property BANC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mBANC = general.nz(dvForm(PA).Row("BANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtBancs, CCBancs, Value) Then
                If general.nz(Value, "") <> "" Then
                    mBANC = general.nz(Value, "")
                    dvForm(PA).Row("NOMBANC") = general.OBN(Value, dtBancs, CNBancs)
                    dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
                Else
                    dvForm(PA).Row("BANC") = ""

                    dvForm(PA).Row("NOMBANC") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("BANC") = ""

                dvForm(PA).Row("NOMBANC") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEBANCS"))
            End If
        End Set
    End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMBANC = general.nz(dvForm(PA).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mLINEA As String
    Public Property LINEA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mLINEA = general.nz(dvForm(PA).Row("LINEA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mLINEA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(LINEA, "") Then
                mLINEA = general.nz(Value, "")
                dvForm(PA).Row("LINEA") = general.nz(mLINEA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDESCUENTO As Boolean
    Public Property DESCUENTO() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCUENTO = dvForm(PA).Row("DESCUENTO")
            Catch ex As Exception : End Try
            Return mDESCUENTO
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> DESCUENTO Then
                mDESCUENTO = Value
                dvForm(PA).Row("DESCUENTO") = mDESCUENTO : guardarDV()
            End If
        End Set
    End Property


#End Region

#Region "INICIALIZAR"

    Public Sub New(ByVal tabla As DataTable, _
                       ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            Me.centro = centro
            sqlSinWhere = "SELECT remesas.*, " & _
                " filiales.DESCRI AS NOMREMESOR, " & _
                " filiales.DOM AS DOMREMESOR, " & _
                " filiales.CP AS CPREMESOR, " & _
                " filiales.POB AS POBREMESOR, " & _
                " filiales.PROV AS PROVREMESOR, " & _
                " bancs.DESCRIPCIO AS NOMBANC, " & _
                " bancs.LOGO, " & _
                " FILIALES.LOGO AS LOGOEMPRESA " & _
                " FROM remesas " & _
                " LEFT JOIN bancs ON (bancs.CODI = remesas.BANC) " & _
                " LEFT JOIN filiales ON (filiales.CODI = remesas.CENTRO) "

            sqlSel = sqlSinWhere & genWhere() & GenOrder() & " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            dvForm.Sort = "CODI"
            CargaTablas()

            IniciarLineas()
            MoverActual()

            'PonerDefaults()
            'DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaRemesas, "CODI", "CODI", dtIdentificadores, True)
            dtIdentificadores.Columns("CODI").Caption = rm.GetString("CODIGO")
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CargaTablas()
        Try
            CargarIdentificadores()
            CargaTabla(tablaBancos, CCBancs, CNBancs, dtBancs, True)
            CargaTabla(tablaEmpresas, "CODI", "DESCRI", dtRemesor, True)
            CargaTabla(tablaClientes, "CODI", "NOM", dtClients, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarLineas()
        Try
            lineasRemesa = New clsLineasRemesa(New aura2k3.ds11.dremesasDataTable, Me.centro, Me.bc, Me)
            cuentasRemesa = New clsCuentasRemesa(New aura2k3.ds11.remesascuentasDataTable, Me.centro, Me.bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "IMRESION"

    Private Function CrearTablaRemesa() As DataTable
        Try
            'Ahora con direcciones de envio
            Dim dt As New DataTable("REMESA")
            Dim dr As DataRow
            Dim i As Integer

            dt.Columns.Add(New DataColumn("CODI"))
            dt.Columns("CODI").DefaultValue = CODI

            dt.Columns.Add(New DataColumn("LOGO"))
            dt.Columns("LOGO").DataType = GetType(Byte())
            dt.Columns("LOGO").DefaultValue = dvForm(PA).Item("LOGO")

            dt.Columns.Add(New DataColumn("LOGOEMPRESA"))
            dt.Columns("LOGOEMPRESA").DataType = GetType(Byte())
            dt.Columns("LOGOEMPRESA").DefaultValue = dvForm(PA).Item("LOGOEMPRESA")

            dt.Columns.Add(New DataColumn("COBRO"))
            dt.Columns("COBRO").DefaultValue = IIf(COBRO = True, 1, 0)

            dt.Columns.Add(New DataColumn("DESCUENTO"))
            dt.Columns("DESCUENTO").DefaultValue = IIf(DESCUENTO = True, 1, 0)

            dt.Columns.Add(New DataColumn("NOMBANC"))
            dt.Columns("NOMBANC").DefaultValue = NOMBANC

            dt.Columns.Add(New DataColumn("OFICINA"))
            dt.Columns("OFICINA").DefaultValue = OFICINA

            dt.Columns.Add(New DataColumn("NOMREMESOR"))
            dt.Columns("NOMREMESOR").DefaultValue = NOMREMESOR

            dt.Columns.Add(New DataColumn("LINEA"))
            dt.Columns("LINEA").DefaultValue = LINEA

            dt.Columns.Add(New DataColumn("DOMREMESOR"))
            dt.Columns("DOMREMESOR").DefaultValue = dvForm(PA).Item("DOMREMESOR")

            dt.Columns.Add(New DataColumn("CPPOB"))
            dt.Columns("CPPOB").DefaultValue = dvForm(PA).Item("CPREMESOR") & " " & dvForm(PA).Item("POBREMESOR") & " (" & dvForm(PA).Item("PROVREMESOR") & ")"

            dt.Columns.Add(New DataColumn("NUMCUENTA"))
            Try
                dt.Columns("NUMCUENTA").DefaultValue = cuentasRemesa.dvForm(0).Item("CUENTA")
            Catch ex As Exception
                dt.Columns("NUMCUENTA").DefaultValue = ""
            End Try

            dt.Columns.Add(New DataColumn("FECHA"))
            dt.Columns("FECHA").DefaultValue = FECHA.ToShortDateString

            dt.Columns.Add(New DataColumn("TOTAL"))
            dt.Columns("TOTAL").DefaultValue = sumaTotal("IMPORT", lineasRemesa.dvForm)

            dt.Columns.Add(New DataColumn("PLAZA"))
            dt.Columns.Add(New DataColumn("NEFECTO"))
            dt.Columns.Add(New DataColumn("IMPORT"))
            dt.Columns.Add(New DataColumn("VENCIM"))
            dt.Columns.Add(New DataColumn("NOMCLIENT"))

            If lineasRemesa.dvForm.Count = 0 Then
                dr = dt.NewRow
                dt.Rows.Add(dr)
            End If
            For i = 0 To lineasRemesa.dvForm.Count - 1
                dr = dt.NewRow
                dr("PLAZA") = lineasRemesa.dvForm(i).Item("PLAZA")
                dr("NEFECTO") = lineasRemesa.dvForm(i).Item("NEFECTO")
                dr("IMPORT") = lineasRemesa.dvForm(i).Item("IMPORT")
                dr("VENCIM") = lineasRemesa.dvForm(i).Item("VENCIM")
                dr("NOMCLIENT") = lineasRemesa.dvForm(i).Item("NOMCLIENT")
                dt.Rows.Add(dr)
            Next

            Dim dsTEMPORAL As New DataSet
            dsTEMPORAL.Tables.Add(dt)
            dsTEMPORAL.WriteXml(CurDir() & "\tablas.xml")
            dsTEMPORAL.WriteXmlSchema(CurDir() & "\tablas.xsd")
            Return dt

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    'Friend Sub PictureFormat(ByVal sender As Object, ByVal e As ConvertEventArgs)
    '    Try
    '        If Not e.Value Is Nothing AndAlso Not IsDBNull(e.Value) Then
    '            Dim img() As Byte = CType(e.Value, Byte())
    '            Dim ms As IO.MemoryStream = New IO.MemoryStream
    '            Dim offset As Integer = 0
    '            ms.Write(img, offset, img.Length - offset)
    '            Dim bmp As Bitmap = New Bitmap(ms)
    '            ms.Close()
    '            e.Value = bmp
    '        Else
    '            Dim bmp As Bitmap = New Bitmap(curdir & "\nada.bmp")
    '            e.Value = bmp
    '        End If

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN()
    '    End Try
    'End Sub
    'Friend Function PictureFormat2(ByVal E As Object, ByVal nombre As String)
    '    Dim ms As IO.MemoryStream
    '    Dim oStream As System.IO.FileStream
    '    Dim img() As Byte
    '    Dim pic As System.Drawing.Bitmap
    '    Try
    '        If Not E Is Nothing AndAlso Not IsDBNull(E) Then
    '            oStream = New System.IO.FileStream(nombre, IO.FileMode.Create)
    '            img = CType(E, Byte())
    '            ms = New IO.MemoryStream
    '            ms.Write(img, 0, img.Length)
    '            pic = New System.Drawing.Bitmap(ms)
    '            ms.WriteTo(oStream)
    '            ms.Close()
    '            oStream.Close()
    '        Else
    '            Dim bmp As Bitmap = New Bitmap(curdir & "\nada.bmp")
    '            bmp.Save(nombre)
    '        End If

    '    Catch ex As Exception
    '        LOG(ex.ToString) : cargando = False : CCN() : ms.Close()
    '    End Try
    'End Function
    Public Function imprimirRemesa() As Drawing.Printing.PrintDocument
        Try
            Dim c1r As New C1.C1Report.C1Report
            'PictureFormat2(dvForm(PA).Item("LOGO"), curdir & "\informes\LOGOBANCO.bmp")
            c1r.Load(CurDir() & "\informes\informes.xml", "REMESA")
            c1r.DataSource.Recordset = CrearTablaRemesa()
            Return (c1r.Document)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse lineasRemesa.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overloads Sub borrar()
        Try
            ACN()
            MyBase.borrar()
            lineasRemesa.BorrarActualDVDetalle(True)
            cuentasRemesa.BorrarActualDVDetalle(True)
            '!!!!!
            'Desremesar()
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow
        Dim cm As CurrencyManager
        Try
            ActualizarOrigen()
            tabla.Clear()
            cm = bc(dvForm)
            drNew = tabla.NewRow()
            drNew.Item("CODI") = ObtenerNuevoID()
            drNew.Item("CENTRO") = centro
            PonerDatosEmpresa(drNew)
            tabla.Rows.Add(drNew)
            cm.Position = 1
            numeroRegistros = numeroRegistros + 1
            Try : guardarDV() : Catch ex As Exception : End Try
            lineasRemesa.tabla.Clear()
            cuentasRemesa.tabla.Clear()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Function GenOrder() As String
        Return " ORDER BY CODI "
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        ret = " WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"
        Return ret
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Return genWhere()
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = CODI
        End If
        Dim cmd As New MySqlCommand(" SELECT " & _
            " (SELECT COUNT(*) " & _
            " FROM " & t() & " AS M2 WHERE " & _
            " M2.CODI < M1.CODI) AS rownum FROM " & t() & " AS M1 WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
        Try
            Dim idx As Object = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1
            Return idx

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            ACN()
            ActualizarOrigen()
            MyBase.SiguienteReg()
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(anterior)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(ultimo)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(primero)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Try
            ACN()
            ActualizarOrigen()
            MyBase.CambiarAReg(id, "", accion)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "CAMBIO CENTRO"

    Public Overloads Sub cambioCentro(ByVal c As String, ByVal irAlPrimerUltimoRegistro As Short)
        Try
            MyBase.cambioCentro(c, irAlPrimerUltimoRegistro)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub PonerDatosEmpresa(ByRef dr As DataRow)
        Dim strSQL As String = ""
        Dim cmd As MySqlCommand
        Dim dar As MySqlDataReader
        Try
            strSQL = "SELECT " & _
                " filiales.DESCRI AS NOMREMESOR, " & _
                " filiales.DOM AS DOMREMESOR, " & _
                " filiales.CP AS CPREMESOR, " & _
                " filiales.POB AS POBREMESOR, " & _
                " filiales.PROV AS PROVREMESOR FROM " & tablaEmpresas & " " & _
                " WHERE filiales.CODI = """ & centro & """ "

            cmd = New MySqlCommand(strSQL, cnn)
            ACN()
            dar = cmd.ExecuteReader
            While dar.Read
                dr("NOMREMESOR") = dar("NOMREMESOR")
                dr("DOMREMESOR") = dar("DOMREMESOR")
                dr("PROVREMESOR") = dar("PROVREMESOR")
                dr("POBREMESOR") = dar("POBREMESOR")
                dr("CPREMESOR") = dar("CPREMESOR")
            End While
            dar.Close()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : dar.Close()
        End Try
    End Sub
    Public Sub MoverActual()
        Try
            If Not PA() = -1 Then lineasRemesa.CambioDetalle(centro, Me)
            If Not PA() = -1 Then cuentasRemesa.CambioDetalle(centro, Me)
            Dim a As New ArrayList
            Dim i As Integer
            a.Add("CUENTA")
            a.Add("TIPO")
            a.Add("DOM")
            a.Add("POB")
            a.Add("CP")
            CargaTabla("BANCSCUENTAS", a, dtCuentas, True, " BANC = """ & BANC & """ ")
            dtCuentas.Columns("CUENTA").Caption = rm.GetString("CUENTA")
            dtCuentas.DefaultView.Sort = "CUENTA"

            If DESCUENTO = True Then : dtCuentas.DefaultView.RowFilter = "TIPO = 'C' AND CENTRO = '" & centro & "'"
            Else : dtCuentas.DefaultView.RowFilter = "TIPO = 'D' AND CENTRO = '" & centro & "'" : End If

            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub CargarVencimientos(ByVal inicio As Date, ByVal final As Date, Optional ByVal cliente As Integer = 0)
        Dim cmdSelectVencim As New MySqlCommand
        Dim dt As New DataTable
        Dim daVencim As New MySqlDataAdapter
        Try
            Dim DC As New DataColumn("REMES")
            DC.DataType = GetType(Boolean)
            dt.Columns.Add(DC)
            cmdSelectVencim.Connection = cnn
            daVencim.SelectCommand = cmdSelectVencim
            If cliente = 0 Then
                cmdSelectVencim.CommandText = "SELECT vencim.*,  " & _
                    " factur.CLIENT AS CODICLIENT, clients.POB as PLAZA, " & _
                    " clients.NOM AS NOMCLIENT  " & _
                    " FROM vencim  " & _
                    " LEFT JOIN factur ON (factur.FRA = vencim.FRA)  " & _
                    " LEFT JOIN clients ON (clients.CODI = factur.CLIENT) " & _
                    " WHERE " & _
                    " factur.DOCUMENT = ""F"" AND " & _
                    " vencim.CENTRO = """ & centro & """ AND " & _
                    " vencim.COMVEN = ""V"" AND " & _
                    " VENCIM.VENCIM > """ & ConvertirAfechaMysql(inicio) & """ AND " & _
                    " VENCIM.VENCIM < """ & ConvertirAfechaMysql(final) & """ " & _
                    " ORDER BY vencim.FRA "
            Else
                cmdSelectVencim.CommandText = "SELECT vencim.*,  " & _
                                    " factur.CLIENT AS CODICLIENT, clients.POB as PLAZA, " & _
                                    " clients.NOM AS NOMCLIENT  " & _
                                    " FROM vencim  " & _
                                    " LEFT JOIN factur ON (factur.FRA = vencim.FRA)  " & _
                                    " LEFT JOIN clients ON (clients.CODI = factur.CLIENT) " & _
                        " WHERE " & _
                        " factur.DOCUMENT = ""F"" AND " & _
                        " factur.CLIENT = " & cliente & " AND " & _
                        " vencim.CENTRO = """ & centro & """ AND " & _
                        " vencim.COMVEN = ""V"" AND " & _
                        " VENCIM.VENCIM > """ & ConvertirAfechaMysql(inicio) & """ AND " & _
                        " VENCIM.VENCIM < """ & ConvertirAfechaMysql(final) & """ " & _
                        " ORDER BY vencim.FRA "
            End If

            cmdSelectVencim.Connection = cnn
            daVencim.SelectCommand = cmdSelectVencim
            daVencim.Fill(dt)
            dt.Columns("REMES").DataType = GetType(Boolean)

            dvVencimientos = New DataView
            dvVencimientos = dt.DefaultView
            dvVencimientos.Sort = "FRA DESC"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub Remetir()
        Try
            If dvVencimientos(PA(dvVencimientos)).Item("REMES") = True Then
                MessageBox.Show(rm.GetString("EFECTOYAREMETIDO"), "", MessageBoxButtons.OK)
            Else
                RemetirActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub RemetirIntervalo(ByVal inicio As Date, ByVal final As Date, Optional ByVal cliente As Integer = 0)
        Dim cmdSel As New MySqlCommand
        Dim daread As MySqlDataReader
        Dim dr As DataRow
        Dim cn2 As New MySql.Data.MySqlClient.MySqlConnection(cnn.ConnectionString)


        Try
            'MYSQ CAMBIO !!!!!!!!
            'cn2.UserId = "root"

            Dim cmd As New MySqlCommand("UPDATE VENCIM SET", cn2)

            cmdSel.Connection = cnn
            cmdSel.CommandText = "SELECT vencim.*, " & _
                    " factur.CLIENT AS CODICLIENT " & _
                    " FROM vencim  " & _
                    " LEFT JOIN factur ON (factur.DOCUMENT = ""F"" AND factur.FRA = vencim.FRA)  " & _
                    " WHERE  " & _
                    " vencim.VENCIM > """ & ConvertirAfechaMysql(inicio) & """ AND  " & _
                    " vencim.VENCIM < """ & ConvertirAfechaMysql(final) & """  AND  " & _
                    " vencim.REMES = ""N"" "

            If cliente <> 0 Then cmdSel.CommandText = cmdSel.CommandText & " AND factur.client = " & cliente & " "
            'cmdSel.CommandText = cmdSel.CommandText & " ORDER BY vencim.FRA"
            ACN()
            cn2.Open()
            daread = cmdSel.ExecuteReader

            While daread.Read
                dr = lineasRemesa.tabla.NewRow

                dr("REMESA") = CODI
                dr("REMES") = True
                dr("VENCIM") = daread("VENCIM")
                dr("IMPORT") = daread("IMPORT")
                dr("LINEA") = lineasRemesa.dvForm.Count + 1
                dr("NEFECTO") = daread("FRA")
                dr("CLIENT") = daread("CODICLIENT")
                dr("CENTRO") = daread("CENTRO")
                dr("DELINEA") = daread("LIN")

                lineasRemesa.tabla.Rows.Add(dr)
                cmd.CommandText = " UPDATE VENCIM " & _
                    " SET REMES = TRUE " & _
                    " WHERE " & _
                    " FRA = """ & daread("FRA") & """ AND " & _
                    " DOCUMENT = ""F"" AND " & _
                    " COMVEN = ""V"" AND " & _
                    " TIPUS = """ & daread("TIPUS") & """ AND " & _
                    " LIN = """ & daread("LIN") & """ AND " & _
                    " CENTRO = """ & daread("CENTRO") & """ "
                cmd.ExecuteNonQuery()

            End While
            daread.Close()
            CCN()
            cn2.Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub RemetirActual()
        Dim cmd As New MySqlCommand("UPDATE VENCIM SET", cnn)
        Try
            ACN()
            cmd.CommandText = " UPDATE VENCIM SET REMES = TRUE WHERE " & _
                    " FRA = """ & dvVencimientos(PA(dvVencimientos)).Item("FRA") & """ " & _
                    " AND DOCUMENT = ""F"" AND " & _
                    " COMVEN = ""V"" AND " & _
                    " TIPUS = """ & dvVencimientos(PA(dvVencimientos)).Item("TIPUS") & """ AND " & _
                    " LIN = """ & dvVencimientos(PA(dvVencimientos)).Item("LIN") & """ AND " & _
                    " CENTRO = """ & dvVencimientos(PA(dvVencimientos)).Item("CENTRO") & """ "
            cmd.ExecuteNonQuery()
            CCN()
            'Ahora se añade a lineas remesa
            Dim dr As DataRow
            dr = lineasRemesa.tabla.NewRow
            dr("REMES") = True
            dr("LINEA") = lineasRemesa.dvForm.Count
            dr("DELINEA") = dvVencimientos(PA(dvVencimientos)).Item("LIN")
            dr("VENCIM") = dvVencimientos(PA(dvVencimientos)).Item("VENCIM")
            dr("IMPORT") = dvVencimientos(PA(dvVencimientos)).Item("IMPORT")
            dr("NEFECTO") = dvVencimientos(PA(dvVencimientos)).Item("FRA")
            dr("CLIENT") = ObtenerCliente(dr("NEFECTO"))

            lineasRemesa.tabla.Rows.Add(dr)
            lineasRemesa.ActualizarOrigen(True, True)
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function ObtenerCliente(ByVal fra As Integer) As Integer
        Dim ret As Integer
        Try
            ACN()
            Dim cmd As New MySqlCommand("SELECT CLIENT FROM FACTUR " & _
                " WHERE  " & _
                " FRA = """ & fra & """ AND  " & _
                " DOCUMENT = ""F"" ", cnn)

            ret = cmd.ExecuteScalar
            CCN()
            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ACN()
            MyBase.ActualizarOrigen(True)
            lineasRemesa.ActualizarOrigen(True, True)
            cuentasRemesa.ActualizarOrigen(True, True)
            CCN()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub


#End Region

End Class