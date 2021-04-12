Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsCartaColoresMuestra
    Inherits clsADO
    Private m_Muestra As clsMuestra

#Region "VARIABLES"

    Friend dtClients As New DataTable("CLIENTS")

#End Region

#Region "CAMPOS"

    Private mMOSTRA As String
    Public Property MOSTRA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mMOSTRA = general.nz(dvForm(pa).Row("MOSTRA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMOSTRA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MOSTRA, "") Then
                mMOSTRA = general.nz(Value, "")
                dvForm(pa).Row("MOSTRA") = general.nz(mMOSTRA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA As String
    Public Property TALLA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTALLA = general.nz(dvForm(pa).Row("TALLA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA, "") Then
                mTALLA = general.nz(Value, "")
                dvForm(pa).Row("TALLA") = general.nz(mTALLA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLAH As Double
    Public Property TALLAH() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLAH = general.nz(dvForm(PA).Row("TALLAH"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLAH, "")
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLAH, "") Then
                mTALLAH = general.nz(Value, "")
                dvForm(PA).Row("TALLAH") = general.nz(mTALLAH, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLAL As Double
    Public Property TALLAL() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLAL = general.nz(dvForm(PA).Row("TALLAL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLAL, "")
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLAL, "") Then
                mTALLAL = general.nz(Value, "")
                dvForm(PA).Row("TALLAL") = general.nz(mTALLAL, "") : guardarDV()
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
                dvForm(pa).Row("DESCRI") = general.nz(mDESCRI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOST As Double
    Public Property COST() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mCOST = nzn(dvForm(pa).Row("COST"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOST, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COST, 0) Then
                mCOST = nzn(Value, 0)
                dvForm(pa).Row("COST") = nzn(mCOST, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mVENDA As Double
    Public Property VENDA() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mVENDA = nzn(dvForm(pa).Row("VENDA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mVENDA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(VENDA, 0) Then
                mVENDA = nzn(Value, 0)
                dvForm(pa).Row("VENDA") = nzn(mVENDA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOLOR = general.nz(dvForm(pa).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLOR, "") Then
                mCOLOR = general.nz(Value, "")
                dvForm(pa).Row("COLOR") = general.nz(mCOLOR, "") : guardarDV()
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
            If esCodigoExistente(dtClients, CCClients, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(pa).Row("NOMCLIENT") = general.OBN(Value, dtClients, CNClients)
                    dvForm(pa).Row("CLIENT") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(pa).Row("CLIENT") = 0

                dvForm(pa).Row("NOMCLIENT") = "" : guardarDV()
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
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNCCODE As String
    Public Property NCCODE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNCCODE = general.nz(dvForm(PA).Row("NCCODE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNCCODE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NCCODE, "") Then
                mNCCODE = general.nz(Value, "")
                dvForm(PA).Row("NCCODE") = general.nz(mNCCODE, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

    Public Sub New(ByVal tabla As aura2k3.ds11.tallaDataTable, _
                   ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal muestra As clsMuestra)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")

        Dim query As String = ""
        Try
            Dim sqlSel As String
            m_Muestra = muestra
            sqlSinWhere = "SELECT * FROM " & tablaTallas '& " Where TALLAH is null "
            sqlSel = sqlSinWhere &
            " WHERE " &
            " CLIENT = " & m_Muestra.CLIENT & " " & " AND " &
            " CENTRO = """ & m_Muestra.centro & """ AND " & ' ORDER BY color" &
            " MOSTRA  = """ & m_Muestra.CODI & """ "


            cmdSel.CommandText = sqlSel
            'dvForm.Sort = "COLOR"
            da.SelectCommand = cmdSel
            da.Fill(tabla)


            ''test
            'Dim i As Integer = 0
            'For i = 0 To tabla.Rows.Count - 1
            '    Dim s As String
            '    Dim h, l As Double
            '    s = tabla.Rows(i)("talla")
            '    Try
            '        h = CDbl(s.ToUpper().Replace(".", ",").Split("X"c)(0))
            '    Catch ex As Exception
            '        h = 0
            '    End Try
            '    Try
            '        l = CDbl(s.ToUpper().Replace(".", ",").Split("X"c)(1))
            '    Catch ex As Exception
            '        l = 0
            '    End Try
            '    'tabla.Rows(i)("tallaH") = h
            '    'tabla.Rows(i)("tallaL") = l
            '    'query = ""
            '    s = "update talla Set TALLAH = " & h.ToString.Replace(",", ".") & ", TALLAL = " & l.ToString.Replace(",", ".") & " Where MOSTRA like '" & tabla.Rows(i)("MOSTRA") & "' and TALLA like '" & tabla.Rows(i)("TALLA") & "' and color like '" & tabla.Rows(i)("color") & "' and client = " & tabla.Rows(i)("client") & " and centro like '" & tabla.Rows(i)("centro") & "' ;" & vbCrLf

            '    If Not s.Contains(vbNullChar) Then
            '        query = query & s
            '    End If
            'Next

            tabla.Columns.Add("DESGLOSEMUESTRA")
            tabla.Columns("DESGLOSEMUESTRA").DefaultValue = "" 'RM.GetString("DESGLOSE")
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub

#Region "ORGANIZAR"

    Friend Sub CambioDetalle(ByVal centro As String, ByVal mues As clsMuestra)
        Try
            Dim sqlSel As String
            m_Muestra = mues
            Me.centro = centro
            sqlSel = sqlSinWhere &
                        " WHERE " &
                        " CLIENT = " & m_Muestra.CLIENT & " " & " AND " &
                        " MOSTRA  = """ & m_Muestra.CODI & """  AND " &
                        " CENTRO = """ & m_Muestra.centro & """ ORDER BY color"

            cmdSel.CommandText = sqlSel
            'dvForm.Sort = "COLOR"
            da.SelectCommand = cmdSel
            tabla.Clear()
            da.Fill(tabla)
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            With dvForm

                .Table.Columns("MOSTRA").DefaultValue = m_Muestra.CODI
                .Table.Columns("CLIENT").DefaultValue = m_Muestra.CLIENT
                .Table.Columns("CENTRO").DefaultValue = m_Muestra.centro

                .Table.Columns("TALLA").DefaultValue = ""

                .Table.Columns("DESCRI").DefaultValue = ""
                .Table.Columns("COST").DefaultValue = 0
                .Table.Columns("VENDA").DefaultValue = 0
                .Table.Columns("COLOR").DefaultValue = 0


            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : ccn()
        End Try
    End Sub
    Friend Overrides Function GenOrder() As String

    End Function
    Friend Overrides Function genWhere() As String

    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String

    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer

    End Function

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
            'Este borra todas las lineas
            BorrarActualDVDetalle()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If general.nz(dvForm(i).Item("MOSTRA"), "") <> general.nz(m_Muestra.CODI, "") Then dvForm(i).Item("MOSTRA") = general.nz(m_Muestra.CODI, "") : cambio = True
                If nzn(dvForm(i).Item("CLIENT"), 0) <> nzn(m_Muestra.CLIENT, 0) Then dvForm(i).Item("CLIENT") = nzn(m_Muestra.CLIENT, 0) : cambio = True
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

End Class
