Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsProveedor
    Inherits clsADO

#Region "CAMPOS"

    Private mCODI As Integer
    Public Property CODI() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mCODI = nzn(dvForm(pa).Row("CODI"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCODI, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CODI, 0) Then
                mCODI = nzn(Value, 0)
                dvForm(pa).Row("CODI") = nzn(mCODI, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNOM As String
    Public Property NOM() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOM = general.nz(dvForm(pa).Row("NOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOM, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOM, "") Then
                mNOM = general.nz(Value, "")
                dvForm(pa).Row("NOM") = general.nz(mNOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDOM As String
    Public Property DOM() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDOM = general.nz(dvForm(pa).Row("DOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOM, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOM, "") Then
                mDOM = general.nz(Value, "")
                dvForm(pa).Row("DOM") = general.nz(mDOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCP As String
    Public Property CP() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCP = general.nz(dvForm(pa).Row("CP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCP, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CP, "") Then
                mCP = general.nz(Value, "")
                dvForm(pa).Row("CP") = general.nz(mCP, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEL As String
    Public Property TEL() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEL = general.nz(dvForm(pa).Row("TEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL, "") Then
                mTEL = general.nz(Value, "")
                dvForm(pa).Row("TEL") = general.nz(mTEL, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFAX As String
    Public Property FAX() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mFAX = general.nz(dvForm(pa).Row("FAX"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFAX, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FAX, "") Then
                mFAX = general.nz(Value, "")
                dvForm(pa).Row("FAX") = general.nz(mFAX, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPOB As String
    Public Property POB() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mPOB = general.nz(dvForm(pa).Row("POB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPOB, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(POB, "") Then
                mPOB = general.nz(Value, "")
                dvForm(pa).Row("POB") = general.nz(mPOB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROV As String
    Public Property PROV() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mPROV = general.nz(dvForm(pa).Row("PROV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPROV, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PROV, "") Then
                mPROV = general.nz(Value, "")
                dvForm(pa).Row("PROV") = general.nz(mPROV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNIF As String
    Public Property NIF() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNIF = general.nz(dvForm(pa).Row("NIF"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNIF, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NIF, "") Then
                mNIF = general.nz(Value, "")
                dvForm(pa).Row("NIF") = general.nz(mNIF, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNOTES As String
    Public Property NOTES() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOTES = general.nz(dvForm(pa).Row("NOTES"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOTES, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOTES, "") Then
                mNOTES = general.nz(Value, "")
                dvForm(pa).Row("NOTES") = general.nz(mNOTES, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCONTACTE As String
    Public Property CONTACTE() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCONTACTE = general.nz(dvForm(pa).Row("CONTACTE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCONTACTE, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CONTACTE, "") Then
                mCONTACTE = general.nz(Value, "")
                dvForm(pa).Row("CONTACTE") = general.nz(mCONTACTE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMA As String
    Public Property FORMA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mFORMA = general.nz(dvForm(pa).Row("FORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtForpag, CCForpag, Value) Then
                If general.nz(Value, "") <> "" Then
                    mFORMA = general.nz(Value, "")
                    dvForm(pa).Row("NOMFORMA") = general.OBN(Value, dtForpag, CNForpag)
                    dvForm(pa).Row("FORMA") = general.nz(Value, "") : guardarDV()
                End If
            Else
                dvForm(pa).Row("FORMA") = ""

                dvForm(pa).Row("NOMFORMA") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEFORPAG"))
            End If
        End Set
    End Property

    Private mNOMFORMA As String
    Public Property NOMFORMA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMFORMA = general.nz(dvForm(pa).Row("NOMFORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMFORMA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA1 As Integer
    Public Property DIA1() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mDIA1 = nzn(dvForm(pa).Row("DIA1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA1, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA1, 0) Then
                mDIA1 = nzn(Value, 0)
                dvForm(pa).Row("DIA1") = nzn(mDIA1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDIA2 As Integer
    Public Property DIA2() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mDIA2 = nzn(dvForm(pa).Row("DIA2"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA2, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA2, 0) Then
                mDIA2 = nzn(Value, 0)
                dvForm(pa).Row("DIA2") = nzn(mDIA2, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDIA3 As Integer
    Public Property DIA3() As Integer
        Get
            If PA = -1 Then Exit Property
            Try
                mDIA3 = nzn(dvForm(pa).Row("DIA3"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA3, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA3, 0) Then
                mDIA3 = nzn(Value, 0)
                dvForm(pa).Row("DIA3") = nzn(mDIA3, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mBANC As String
    Public Property BANC() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mBANC = general.nz(dvForm(pa).Row("BANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If esCodigoExistente(dtBancs, CCBancs, Value) Then
                If general.nz(Value, "") <> "" Then
                    mBANC = general.nz(Value, "")
                    dvForm(pa).Row("NOMBANC") = general.OBN(Value, dtBancs, CNBancs)
                    dvForm(pa).Row("BANC") = general.nz(Value, "") : guardarDV()
                End If
            Else
                dvForm(pa).Row("BANC") = ""

                dvForm(pa).Row("NOMBANC") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEBANCS"))
            End If
        End Set
    End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mNOMBANC = general.nz(dvForm(pa).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(pa).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(pa).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOFI As String
    Public Property COFI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCOFI = general.nz(dvForm(pa).Row("COFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOFI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COFI, "") Then
                mCOFI = general.nz(Value, "")
                dvForm(pa).Row("COFI") = general.nz(mCOFI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOFI As String
    Public Property OFI() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mOFI = general.nz(dvForm(pa).Row("OFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOFI, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OFI, "") Then
                mOFI = general.nz(Value, "")
                dvForm(pa).Row("OFI") = general.nz(mOFI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDC As String
    Public Property DC() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mDC = general.nz(dvForm(pa).Row("DC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDC, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DC, "") Then
                mDC = general.nz(Value, "")
                dvForm(pa).Row("DC") = general.nz(mDC, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCTA As String
    Public Property CTA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mCTA = general.nz(dvForm(pa).Row("CTA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCTA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CTA, "") Then
                mCTA = general.nz(Value, "")
                dvForm(pa).Row("CTA") = general.nz(mCTA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mIVA = general.nz(dvForm(pa).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IVA, "") Then
                mIVA = general.nz(Value, "")
                dvForm(pa).Row("IVA") = general.nz(mIVA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mSUBCTA As Double
    Public Property SUBCTA() As Double
        Get
            If PA = -1 Then Exit Property
            Try
                mSUBCTA = nzn(dvForm(pa).Row("SUBCTA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mSUBCTA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(SUBCTA, 0) Then
                mSUBCTA = nzn(Value, 0)
                dvForm(pa).Row("SUBCTA") = nzn(mSUBCTA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTRASPAS As String
    Public Property TRASPAS() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTRASPAS = general.nz(dvForm(pa).Row("TRASPAS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTRASPAS, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TRASPAS, "") Then
                mTRASPAS = general.nz(Value, "")
                dvForm(pa).Row("TRASPAS") = general.nz(mTRASPAS, "") : guardarDV()
            End If
        End Set
    End Property

    Private mWEB As String
    Public Property WEB() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mWEB = general.nz(dvForm(pa).Row("WEB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mWEB, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(WEB, "") Then
                mWEB = general.nz(Value, "")
                dvForm(pa).Row("WEB") = general.nz(mWEB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL1 As String
    Public Property EMAIL1() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mEMAIL1 = general.nz(dvForm(pa).Row("EMAIL1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL1, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL1, "") Then
                mEMAIL1 = general.nz(Value, "")
                dvForm(pa).Row("EMAIL1") = general.nz(mEMAIL1, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL2 As String
    Public Property EMAIL2() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mEMAIL2 = general.nz(dvForm(pa).Row("EMAIL2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL2, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL2, "") Then
                mEMAIL2 = general.nz(Value, "")
                dvForm(pa).Row("EMAIL2") = general.nz(mEMAIL2, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPAIS As String
    Public Property PAIS() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mPAIS = general.nz(dvForm(pa).Row("PAIS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPAIS, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PAIS, "") Then
                mPAIS = general.nz(Value, "")
                dvForm(pa).Row("PAIS") = general.nz(mPAIS, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Friend dtBancs As New DataTable("BANCS")
    Friend dtForpag As New DataTable("FORPAG")

#End Region

#Region "INICIAR"

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            sqlSinWhere = "SELECT prove.*, " & _
                    " filiales.DESCRI AS NOMCENTRO " & _
                    " FROM prove " & _
                    " LEFT JOIN filiales ON (filiales.CODI = prove.CENTRO) "
            Try
                UltimoCodigo = CargaParametro("proveedor")
                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    numeroRegistroActual = 0
                    sqlSel = sqlSinWhere & _
                            " WHERE PROVE.CENTRO = """ & centro & """ ORDER BY PROVE.CODI " & _
                            " LIMIT 1"
                Else
                    sqlSel = sqlSinWhere & _
                              " WHERE PROVE.CENTRO = """ & centro & """ AND PROVE.CODI = """ & UltimoCodigo & """ ORDER BY PROVE.CODI " & _
                              " LIMIT 1"
                End If

            Catch ex As Exception
                sqlSel = sqlSinWhere & _
                            " WHERE PROVE.CENTRO = """ & centro & """ ORDER BY PROVE.CODI " & _
                            " LIMIT 1"
            End Try

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            numeroRegistros = obtenerNumeroReg(tabla.TableName, "")
            da.Fill(tabla)
            CargarIdentificadores()
            CargarTablas()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaProveedores, CCProve, CNProve, dtIdentificadores, True)
            dtIdentificadores.Columns(CCProve).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNProve).Caption = rm.GetString("NOM")
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = CNProve

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CargarTablas()
        Try
            CargaTabla(tablaFormaPago, CCForpag, CNForpag, dtForpag, True)
            CargaTabla(tablaBancos, CCBancs, CNBancs, dtBancs, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "DESPLAZAMIENTO"

    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            NOMBANC = general.OBN(BANC, dtBancs, CNBancs)
            NOMFORMA = general.OBN(FORMA, dtForpag, CNForpag)

            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
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
        Try
            MyBase.CambiarAReg(id, "", accion)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Public Overloads Sub NuevoRegistro()
        Try
            MyBase.NuevoRegistro()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
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
            " FROM " & t() & " AS M2 WHERE " & _
            " M2.CODI < M1.CODI) AS rownum FROM " & t() & " AS M1 WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
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

    Public Sub CancelarCambios()
        Try
            guardarDV()
            tabla.RejectChanges()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
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

#End Region

End Class
