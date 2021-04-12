Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsTransportista
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

    Private mTEL2 As String
    Public Property TEL2() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTEL2 = general.nz(dvForm(pa).Row("TEL2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL2, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL2, "") Then
                mTEL2 = general.nz(Value, "")
                dvForm(pa).Row("TEL2") = general.nz(mTEL2, "") : guardarDV()
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

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(pa).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OBSERV, "") Then
                mOBSERV = general.nz(Value, "")
                dvForm(pa).Row("OBSERV") = general.nz(mOBSERV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMAT As String
    Public Property FORMAT() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mFORMAT = general.nz(dvForm(pa).Row("FORMAT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMAT, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FORMAT, "") Then
                mFORMAT = general.nz(Value, "")
                dvForm(pa).Row("FORMAT") = general.nz(mFORMAT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL As String
    Public Property EMAIL() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mEMAIL = general.nz(dvForm(pa).Row("EMAIL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL, "")
        End Get
        Set(ByVal Value As String)
            If PA = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL, "") Then
                mEMAIL = general.nz(Value, "")
                dvForm(pa).Row("EMAIL") = general.nz(mEMAIL, "") : guardarDV()
            End If
        End Set
    End Property


#End Region

#Region "INICIAR"

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            sqlSinWhere = "SELECT trans.*, " & _
                     " filiales.DESCRI AS NOMCENTRO " & _
                     " FROM trans " & _
                     " LEFT JOIN filiales ON (filiales.CODI = trans.CENTRO) "
            Try
                UltimoCodigo = CargaParametro("transportista")

                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    numeroRegistroActual = 0
                    sqlSel = sqlSinWhere & _
                            " WHERE trans.CENTRO = """ & centro & """ ORDER BY trans.CODI " & _
                            " LIMIT 1"
                Else
                    sqlSel = sqlSinWhere & _
                              " WHERE trans.CENTRO = """ & centro & """ AND trans.CODI = """ & UltimoCodigo & """ ORDER BY trans.CODI " & _
                              " LIMIT 1"
                End If

            Catch ex As Exception
                sqlSel = sqlSinWhere & _
                            " WHERE trans.CENTRO = """ & centro & """ ORDER BY trans.CODI " & _
                            " LIMIT 1"
            End Try

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CargarIdentificadores()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaTransportistas, CCTrans, CNTrans, dtIdentificadores, True)
            dtIdentificadores.Columns(CCTrans).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNTrans).Caption = rm.GetString("NOM")
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = CNTrans

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            sql = "SELECT CODI FROM " & t() & " WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"
            MyBase.CambiarAReg(id, "", accion)
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
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
    Public Overloads Sub NuevoRegistro()
        Try
            MyBase.NuevoRegistro()
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

End Class
