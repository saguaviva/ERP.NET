Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsManipulacion
    Inherits clsADO

#Region "CAMPOS"

        '+----------+--------------+------+-----+---------+-------+
        '| Field    | Type         | Null | Key | Default | Extra |
        '+----------+--------------+------+-----+---------+-------+
        '| TIPUS    | char(1)      | NO   | PRI |         |       |
        '| DOCUMENT | char(1)      | NO   | PRI |         |       |
        '| FRA      | int(11)      | NO   | PRI | 0       |       |
        '| CENTRO   | char(1)      | NO   | PRI | C       |       |
        '| NLINEA   | int(11)      | NO   | PRI | 0       |       |
        '| MOSTRA   | varchar(10)  | YES  |     | NULL    |       |
        '| DESCRI   | varchar(50)  | YES  |     | NULL    |       |
        '| TALLA    | varchar(10)  | YES  |     | NULL    |       |
        '| UNITATS  | double       | YES  |     | NULL    |       |
        '| PREU     | double       | YES  |     | NULL    |       |
        '| DTE      | double       | YES  |     | NULL    |       |
        '| IMPORT   | double       | YES  |     | NULL    |       |
        '| COMAN    | int(11)      | YES  |     | NULL    |       |
        '| ALBAR    | int(11)      | YES  |     | NULL    |       |
        '| FACTURA  | int(11)      | YES  |     | NULL    |       |
        '| ESTOC    | char(1)      | YES  |     | NULL    |       |
        '| COLOR    | varchar(10)  | YES  |     | NULL    |       |
        '| KM       | char(1)      | YES  |     | NULL    |       |
        '| NTRAS    | int(11)      | YES  |     | NULL    |       |
        '| PERREBRE | double(15,2) | YES  |     | NULL    |       |
        '| REBUT    | double(15,2) | YES  |     | NULL    |       |
        '+----------+--------------+------+-----+---------+-------+
        '20 rows

    Private mMODEL As String
    Public Property MODEL() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mMODEL = general.nz(dvForm(pa).Row("MODEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMODEL, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MODEL, "") Then
                mMODEL = general.nz(Value, "")
                dvForm(pa).Row("MODEL") = general.nz(mMODEL, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mSERIE As String
    Public Property SERIE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mSERIE = general.nz(dvForm(pa).Row("SERIE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSERIE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(SERIE, "") Then
                mSERIE = general.nz(Value, "")
                dvForm(pa).Row("SERIE") = general.nz(mSERIE, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mTEMPORADA As String
    Public Property TEMPORADA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEMPORADA = general.nz(dvForm(pa).Row("TEMPORADA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEMPORADA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEMPORADA, "") Then
                mTEMPORADA = general.nz(Value, "")
                dvForm(pa).Row("TEMPORADA") = general.nz(mTEMPORADA, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(pa).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtCLIENTS, CCCLIENTS, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(pa).Row("NOMCLIENT") = general.OBN(Value, dtCLIENTS, CNCLIENTS)
                    dvForm(pa).Row("CLIENT") = nzn(Value, 0) : GuardarDV()
                Else
                    dvForm(pa).Row("CLIENT") = DBNull.value

                    dvForm(pa).Row("NOMCLIENT") = "" : GuardarDV()
                End If
            Else
                dvForm(pa).Row("CLIENT") = DBNull.value

                dvForm(pa).Row("NOMCLIENT") = "" : GuardarDV()
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(pa).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : GuardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(pa).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(PREU, 0) Then
                mPREU = nzn(Value, 0)
                dvForm(pa).Row("PREU") = nzn(mPREU, 0) : GuardarDV()
            End If
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDESCRI = general.nz(dvForm(pa).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRI, "") Then
                mDESCRI = general.nz(Value, "")
                dvForm(pa).Row("DESCRI") = general.nz(mDESCRI, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mCODI As String
    Public Property CODI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = general.nz(dvForm(pa).Row("CODI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCODI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CODI, "") Then
                mCODI = general.nz(Value, "")
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : GuardarDV()
            End If
        End Set
    End Property

    Private mNLINEA As Integer
    Public Property NLINEA() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mNLINEA = nzn(dvForm(pa).Row("NLINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNLINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NLINEA, 0) Then
                mNLINEA = nzn(Value, 0)
                dvForm(pa).Row("NLINEA") = nzn(mNLINEA, 0) : GuardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Friend dtClients As New DataTable("CLIENTS")
    Public modelo As clsModelo

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal p As clsModelo)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try

            modelo = p
            Me.centro = modelo.centro
            modelo.DormirHandlers()
            sqlSinWhere = "SELECT MMANI.*, " & _
                            " filiales.DESCRI AS NOMCENTRO " & _
                            " FROM MMANI " & _
                            " LEFT JOIN filiales ON (filiales.CODI = MMANI.CENTRO) "

            sqlSel = sqlSinWhere & _
                        " WHERE MMANI.MODEL = """ & modelo.CODI & """ AND " & _
                            " MMANI.TEMPORADA = """ & modelo.TEMPORADA & """ AND " & _
                            " MMANI.SERIE = """ & modelo.SERIE & """ AND " & _
                            " MMANI.CLIENT = """ & modelo.CLIENT & """ AND " & _
                            " MMANI.CENTRO = """ & modelo.centro & """ "

            cmdSel.CommandText = sqlSel

            dvForm.Sort = "NLINEA ASC"
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CreaTablas()
            PonerDefaults()
            AddHandler tabla.RowChanged, AddressOf RowChanged
            modelo.DespertarHandlers()
            'dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "ORGANIZAR"

    Friend Sub RowChanged(ByVal sender As Object, ByVal e As System.data.DataRowChangeEventArgs)

        Try
            modelo.DormirHandlers()
            If NLINEA = 10000 Then
                Try
                    If dvForm(dvForm.Count - 2).Item("NLINEA") = -1 Then
                        NLINEA = 1
                    Else
                        NLINEA = dvForm(dvForm.Count - 2).Item("NLINEA") + 1
                    End If

                Catch ex As Exception
                    NLINEA = 1
                End Try
            End If
            modelo.DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            With dvForm
                .Table.Columns("MODEL").DefaultValue = modelo.CODI
                .Table.Columns("NLINEA").DefaultValue = 10000
                .Table.Columns("CLIENT").DefaultValue = modelo.CLIENT
                .Table.Columns("CODI").DefaultValue = ""
                .Table.Columns("DESCRI").DefaultValue = ""
                .Table.Columns("PREU").DefaultValue = 0
                .Table.Columns("SERIE").DefaultValue = modelo.SERIE
                .Table.Columns("TEMPORADA").DefaultValue = modelo.TEMPORADA
                .Table.Columns("CENTRO").DefaultValue = modelo.centro
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "TABLAS ELECCION"

    Private Sub CreaTablas()
    End Sub

#End Region

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
    Public Overrides Sub borrar()
        Try
            BorrarActualDV()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & modelo.centro & """"

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return ""
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = MODEL
        End If

        Dim cmd As New MySqlCommand(" SELECT " & _
           " (SELECT COUNT(*) " & _
           " FROM " & tabla.TableName & " AS M2 WHERE " & _
           " M2.CODI < M1.CODI AND  " & WCNoTabla() & " ) AS rownum FROM " & tabla.TableName & " AS M1  WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
        Try
            Dim idx As Object = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1
            Return idx '- 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Sub CambioDetalle(ByVal centro As String, ByVal PA As clsModelo)
        Dim sqlSel As String
        Try
            modelo = PA
            Me.centro = modelo.centro
            sqlSel = sqlSinWhere & _
                        " WHERE MMANI.MODEL = """ & modelo.CODI & """ AND " & _
                            " MMANI.TEMPORADA = """ & modelo.TEMPORADA & """ AND " & _
                            " MMANI.SERIE = """ & modelo.SERIE & """ AND " & _
                            " MMANI.CLIENT = """ & modelo.CLIENT & """ AND " & _
                            " MMANI.CENTRO = """ & modelo.centro & """ "


            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            dvForm.Sort = "NLINEA ASC"
            tabla.Clear()
            da.Fill(tabla)
            PonerDefaults()
            'dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If general.nz(dvForm(i).Item("MODEL"), "") <> general.nz(modelo.CODI, 0) Then dvForm(i).Item("MODEL") = general.nz(modelo.CODI, 0) : cambio = True
                If general.nz(dvForm(i).Item("SERIE"), "") <> general.nz(modelo.SERIE, "") Then dvForm(i).Item("SERIE") = general.nz(modelo.SERIE, "") : cambio = True
                If general.nz(dvForm(i).Item("TEMPORADA"), "") <> general.nz(modelo.TEMPORADA, "") Then dvForm(i).Item("TEMPORADA") = general.nz(modelo.TEMPORADA, "") : cambio = True
                If general.nz(dvForm(i).Item("CENTRO"), "") <> general.nz(modelo.centro, "") Then dvForm(i).Item("CENTRO") = general.nz(modelo.centro, "") : cambio = True
            Next
            If cambio Then guardarDV()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ActualizarDetalle()
            MyBase.ActualizarOrigen(True, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "CALCULOS"

    Public Sub HacerCalculosLineasPAF(ByVal columnaModificada As String)
        Dim unidades As Double
        Dim recibido As Double
        Dim porrecibir As Double
        Try
            modelo.DormirHandlers()
            guardarDV()
            modelo.DespertarHandlers()
            'hayCambios = False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

End Class