Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsFornitura
    Inherits clsADO

#Region "VARIABLES"

    Public dtClients As New DataTable
    Public dtProve As New DataTable
    Friend detalleForni As clsDetalleForni

#End Region

    'Los campos son los mismos que filcol excepto que observ es para la referencia del proveedor, y color es para la mida
#Region "CAMPOS"

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
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : guardarDV()
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
                dvForm(pa).Row("PREU") = nzn(mPREU, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(pa).Row(tablaProveedores), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtProve, CCProve, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(pa).Row("NOMPROVE") = general.OBN(Value, dtProve, CNProve)
                    dvForm(PA).Row("PROVE") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(pa).Row("PROVE") = 0

                dvForm(pa).Row("NOMPROVE") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMPROVE = general.nz(dvForm(pa).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMPROVE = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREFPRO As String
    Public Property REFPRO() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mREFPRO = general.nz(dvForm(pa).Row("REFPRO"), "")
            Catch ex As Exception : End Try
            Return general.nz(mREFPRO, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(REFPRO, "") Then
                mREFPRO = general.nz(Value, "")
                dvForm(pa).Row("REFPRO") = general.nz(mREFPRO, "") : guardarDV()
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
                dvForm(pa).Row("MODEL") = general.nz(mMODEL, "") : guardarDV()
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
                dvForm(pa).Row("SERIE") = general.nz(mSERIE, "") : guardarDV()
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
                dvForm(pa).Row("TEMPORADA") = general.nz(mTEMPORADA, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            sqlSinWhere = "SELECT forni.* " & _
                        " FROM forni "

            sqlSel = sqlSinWhere & _
                        " WHERE forni.CENTRO = """ & centro & """ ORDER BY forni.CODI " & _
                        " LIMIT 1"

            cmdSel.CommandText = sqlSel

            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CargarIdentificadores()
            CargaTabla(tablaClientes, CCClients, CNClients, dtClients)
            CargaTabla(tablaProveedores, CCProve, CNProve, dtProve)
            PonerNombres()
            detalleForni = New clsDetalleForni(New aura2k3.ds11.filcolDataTable, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaFornituras, CCForni, CNForni, dtIdentificadores, True)
            dtIdentificadores.Columns(CCForni).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNForni).Caption = rm.GetString("DESCRIPCION")

            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = CNForni

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            sql = "SELECT CODI FROM TALLERS WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"
            MyBase.CambiarAReg(id, "", accion)
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "PONERNOMBRES"

    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            NOMCLIENT = general.OBN(CLIENT, dtClients)
            NOMPROVE = general.OBN(PROVE, dtProve)
            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
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
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY CODI "
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = CODI
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

#End Region

#Region "ORGANIZAR"

    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen()
            detalleForni.ActualizarOrigen(True, True)
            MoverActual()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub borrar()
        Try
            MyBase.borrar()
            detalleForni.borrar() : ActualizarOrigen()
            'MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CanviarCartaColores()
        Try
            detalleForni.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            CanviarCartaColores()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub NuevoRegistro()
        Try
            MyBase.NuevoRegistro()
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Function cambioCentro(ByVal centro As Char, ByVal iralregistro As Integer) As Boolean
        Try
            If MyBase.cambioCentro(centro, iralregistro) Then
                MoverActual()
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

End Class
