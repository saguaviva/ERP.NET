Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsMaquinasMuestra

    Inherits clsADO

#Region "VARIABLES"

    Friend dtClients As New DataTable("CLIENTS")
    Public m_Desglose As clsDesgloseMuestra
    Public m_Muestra As clsMuestra

#End Region

#Region "CAMPOS"

    Private mMOSTRA As String
    Public Property MOSTRA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mMOSTRA = general.nz(dvForm(PA).Row("MOSTRA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMOSTRA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MOSTRA, "") Then
                mMOSTRA = general.nz(Value, "")
                dvForm(PA).Row("MOSTRA") = general.nz(mMOSTRA, "") : guardarDV()
            End If
        End Set
    End Property
    Private mCORTES As String
    Public Property CORTES() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCORTES = general.nz(dvForm(PA).Row("CORTES"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCORTES, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CORTES, "") Then
                mCORTES = general.nz(Value, "")
                dvForm(PA).Row("CORTES") = general.nz(mCORTES, "") : guardarDV()
            End If
        End Set
    End Property
    Private mDATA As Date
    Public Property DATA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mDATA = dvForm(PA).Row("DATA")
            Catch ex As Exception : End Try
            Return mDATA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            If Value <> DATA Then
                mDATA = Value
                dvForm(PA).Row("DATA") = mDATA : guardarDV()
            End If
        End Set
    End Property

    Private mNOTAS As String
    Public Property NOTAS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOTAS = general.nz(dvForm(PA).Row("NOTAS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOTAS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOTAS, "") Then
                mNOTAS = general.nz(Value, "")
                dvForm(PA).Row("NOTAS") = general.nz(mNOTAS, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDISCO As String
    Public Property DISCO() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDISCO = general.nz(dvForm(PA).Row("DISCO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDISCO, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DISCO, "") Then
                mDISCO = general.nz(Value, "")
                dvForm(PA).Row("DISCO") = general.nz(mDISCO, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA As String
    Public Property TALLA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA = general.nz(dvForm(PA).Row("TALLA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA, "") Then
                mTALLA = general.nz(Value, "")
                dvForm(PA).Row("TALLA") = general.nz(mTALLA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mAGULLES As Double
    Public Property AGULLES() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mAGULLES = nzn(dvForm(PA).Row("AGULLES"), 0)
            Catch ex As Exception : End Try
            Return nzn(mAGULLES, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(AGULLES, 0) Then
                mAGULLES = nzn(Value, 0)
                dvForm(PA).Row("AGULLES") = nzn(mAGULLES, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDAVANT As String
    Public Property DAVANT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDAVANT = general.nz(dvForm(PA).Row("DAVANT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDAVANT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DAVANT, "") Then
                mDAVANT = general.nz(Value, "")
                dvForm(PA).Row("DAVANT") = general.nz(mDAVANT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDARRERA As String
    Public Property DARRERA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDARRERA = general.nz(dvForm(PA).Row("DARRERA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDARRERA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DARRERA, "") Then
                mDARRERA = general.nz(Value, "")
                dvForm(PA).Row("DARRERA") = general.nz(mDARRERA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mVELOSITAT As Double
    Public Property VELOSITAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mVELOSITAT = nzn(dvForm(PA).Row("VELOSITAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mVELOSITAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(VELOSITAT, 0) Then
                mVELOSITAT = nzn(Value, 0)
                dvForm(PA).Row("VELOSITAT") = nzn(mVELOSITAT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTEMPS As Double
    Public Property TEMPS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEMPS = nzn(dvForm(PA).Row("TEMPS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTEMPS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TEMPS, 0) Then
                mTEMPS = nzn(Value, 0)
                dvForm(PA).Row("TEMPS") = nzn(mTEMPS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mIMPORT As Double
    Public Property IMPORT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mIMPORT = nzn(dvForm(PA).Row("IMPORT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mIMPORT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(IMPORT, 0) Then
                mIMPORT = nzn(Value, 0)
                dvForm(PA).Row("IMPORT") = nzn(mIMPORT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOLOR = general.nz(dvForm(PA).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLOR, "") Then
                mCOLOR = general.nz(Value, "")
                dvForm(PA).Row("COLOR") = general.nz(mCOLOR, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(PA).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtClients, CCClients, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(PA).Row("NOMCLIENT") = general.OBN(Value, dtClients, CNClients)
                    dvForm(PA).Row("CLIENT") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("CLIENT") = 0

                dvForm(PA).Row("NOMCLIENT") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(PA).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "CAMPOS PROPIOS"

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(PA).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If tabla.GetChanges Is Nothing Then
                mPREU = nzn(Value, 0)
                dvForm(PA).Row("PREU") = nzn(Value, 0) : guardarDV()
                tabla.AcceptChanges()
            Else
                mPREU = nzn(Value, 0)
                dvForm(PA).Row("PREU") = nzn(Value, 0) : guardarDV()
            End If

        End Set
    End Property
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
    Friend Overrides Function GenOrder() As String
    End Function
    Friend Overrides Function genWhere() As String

    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String

    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
    End Function
#End Region

    Public Sub New(ByVal tabla As DataTable, _
                    ByVal centro As String, ByRef bindingcontext As BindingContext, _
                    ByVal muestra As clsMuestra, ByVal desglose As clsDesgloseMuestra)

        MyBase.New(tabla, centro, bindingcontext, "CODI")
        Dim sqlSel As String
        Try
            m_Muestra = muestra
            m_Desglose = desglose

            sqlSinWhere = "SELECT " & tablaMaq & ".*, " & _
                " " & tablaMaquinas & ".PREU, " & _
                " " & tablaMaquinas & ".DESCRI " & _
                " FROM " & tablaMaq & " " & _
                " LEFT JOIN " & tablaMaquinas & " ON (" & tablaMaq & ".CLIENT = """ & muestra.CLIENT & """ " & _
                " AND " & tablaMaq & ".MOSTRA = """ & muestra.CODI & """  AND " & _
                " " & tablaMaq & ".TALLA = """ & m_Muestra.cartaColores.TALLA & """  AND " & _
                " " & tablaMaq & ".COLOR = """ & m_Muestra.cartaColores.COLOR & """  AND " & _
                " " & tablaMaquinas & ".CODI = """ & muestra.MAQUINA & """) WHERE " & tablaMaq & ".CLIENT = """ & muestra.CLIENT & """ " & _
                " AND " & tablaMaq & ".MOSTRA = """ & muestra.CODI & """  AND " & _
                " " & tablaMaq & ".TALLA = """ & m_Muestra.cartaColores.TALLA & """  AND " & _
                " " & tablaMaq & ".COLOR = """ & m_Muestra.cartaColores.COLOR & """ "

            sqlSel = sqlSinWhere

            ACN()

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel

            da.Fill(tabla)
            dvForm = tabla.DefaultView
            dvForm.Sort = "COLOR"
            Try
                tabla.Columns.Add("ELEGIRHILO")
            Catch ex As Exception

            End Try
            tabla.AcceptChanges()
            If tabla.Rows.Count = 0 Then
                NuevoRegistro()
            End If
            DesperatarHandlers()

            'CargaTabla(tablaProveedores, CCProve, CNProve, dtProve, True)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Sub CanvioColumna(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs)
        Try
            DormirHandlers()
            guardarDV()

            IMPORT = roundnum(TEMPS * PREU, 2)
            m_Muestra.cartaColores.COST = roundnum(IMPORT + sumaTotal("IMPORT", m_Desglose.dvForm), 2)
            m_Muestra.cartaColores.VENDA = roundnum(100 * m_Muestra.cartaColores.COST / (100 - m_Muestra.MARGE), 2)

            DesperatarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : DesperatarHandlers()
        End Try

    End Sub
    Private Sub DesperatarHandlers()
        Try
            AddHandler tabla.ColumnChanged, AddressOf CanvioColumna
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub DormirHandlers()
        Try
            RemoveHandler tabla.ColumnChanged, AddressOf CanvioColumna
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function obtenerPrecioMaquina() As Double
        Dim cmd As New MySqlCommand("SELECT PREU FROM MAQUI WHERE CODI = """ & m_Muestra.MAQUINA & """ ", cnn)

        Try
            Return cmd.ExecuteScalar
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow
        Dim cm As CurrencyManager
        Try
            ActualizarOrigen()
            tabla.Clear()
            cm = bc(dvForm)
            drNew = tabla.NewRow()

            drNew.Item("CLIENT") = m_Muestra.CLIENT
            drNew.Item("MOSTRA") = m_Muestra.CODI
            drNew.Item("TALLA") = m_Muestra.cartaColores.TALLA
            drNew.Item("COLOR") = m_Muestra.cartaColores.COLOR
            drNew.Item("CENTRO") = centro
            drNew.Item("PREU") = obtenerPrecioMaquina()

            tabla.Rows.Add(drNew)
            cm.Position = 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try

        Catch ex As Exception

            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

End Class
