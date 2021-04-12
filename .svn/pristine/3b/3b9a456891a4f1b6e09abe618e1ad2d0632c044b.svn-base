Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsTaller
    Inherits clsADO

#Region "CAMPOS"
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

    Private mNOM As String
    Public Property NOM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOM = general.nz(dvForm(PA).Row("NOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOM, "") Then
                mNOM = general.nz(Value, "")
                dvForm(PA).Row("NOM") = general.nz(mNOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDOM As String
    Public Property DOM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDOM = general.nz(dvForm(PA).Row("DOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOM, "") Then
                mDOM = general.nz(Value, "")
                dvForm(PA).Row("DOM") = general.nz(mDOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPOB As String
    Public Property POB() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPOB = general.nz(dvForm(PA).Row("POB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPOB, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(POB, "") Then
                mPOB = general.nz(Value, "")
                dvForm(PA).Row("POB") = general.nz(mPOB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCP As String
    Public Property CP() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCP = general.nz(dvForm(PA).Row("CP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCP, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CP, "") Then
                mCP = general.nz(Value, "")
                dvForm(PA).Row("CP") = general.nz(mCP, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROV As String
    Public Property PROV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPROV = general.nz(dvForm(PA).Row("PROV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPROV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PROV, "") Then
                mPROV = general.nz(Value, "")
                dvForm(PA).Row("PROV") = general.nz(mPROV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEL As String
    Public Property TEL() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEL = general.nz(dvForm(PA).Row("TEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL, "") Then
                mTEL = general.nz(Value, "")
                dvForm(PA).Row("TEL") = general.nz(mTEL, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFAX As String
    Public Property FAX() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFAX = general.nz(dvForm(PA).Row("FAX"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFAX, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FAX, "") Then
                mFAX = general.nz(Value, "")
                dvForm(PA).Row("FAX") = general.nz(mFAX, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNIF As String
    Public Property NIF() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNIF = general.nz(dvForm(PA).Row("NIF"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNIF, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NIF, "") Then
                mNIF = general.nz(Value, "")
                dvForm(PA).Row("NIF") = general.nz(mNIF, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(PA).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OBSERV, "") Then
                mOBSERV = general.nz(Value, "")
                dvForm(PA).Row("OBSERV") = general.nz(mOBSERV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMAT As String
    Public Property FORMAT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFORMAT = general.nz(dvForm(PA).Row("FORMAT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMAT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FORMAT, "") Then
                mFORMAT = general.nz(Value, "")
                dvForm(PA).Row("FORMAT") = general.nz(mFORMAT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mWEB As String
    Public Property WEB() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mWEB = general.nz(dvForm(PA).Row("WEB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mWEB, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(WEB, "") Then
                mWEB = general.nz(Value, "")
                dvForm(PA).Row("WEB") = general.nz(mWEB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL1 As String
    Public Property EMAIL1() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mEMAIL1 = general.nz(dvForm(PA).Row("EMAIL1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL1, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL1, "") Then
                mEMAIL1 = general.nz(Value, "")
                dvForm(PA).Row("EMAIL1") = general.nz(mEMAIL1, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL2 As String
    Public Property EMAIL2() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mEMAIL2 = general.nz(dvForm(PA).Row("EMAIL2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL2, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL2, "") Then
                mEMAIL2 = general.nz(Value, "")
                dvForm(PA).Row("EMAIL2") = general.nz(mEMAIL2, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPAIS As String
    Public Property PAIS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPAIS = general.nz(dvForm(PA).Row("PAIS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPAIS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PAIS, "") Then
                mPAIS = general.nz(Value, "")
                dvForm(PA).Row("PAIS") = general.nz(mPAIS, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "VARIABLES"

    Friend acabados As clsAcabados

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            sqlSinWhere = "SELECT tallers.*, " & _
                            " filiales.DESCRI AS NOMCENTRO " & _
                            " FROM tallers " & _
                            " LEFT JOIN filiales ON (filiales.CODI = tallers.CENTRO) "
            Try
                UltimoCodigo = CargaParametro("taller")

                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    numeroRegistroActual = 0
                    sqlSel = sqlSinWhere & _
                            " WHERE tallers.CENTRO = """ & centro & """ ORDER BY tallers.CODI " & _
                            " LIMIT 1"
                Else
                    sqlSel = sqlSinWhere & _
                              " WHERE tallers.CENTRO = """ & centro & """ AND tallers.CODI = """ & UltimoCodigo & """ ORDER BY tallers.CODI " & _
                              " LIMIT 1"
                End If

            Catch ex As Exception
                sqlSel = sqlSinWhere & _
                            " WHERE tallers.CENTRO = """ & centro & """ ORDER BY tallers.CODI " & _
                            " LIMIT 1"
            End Try

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CargarIdentificadores()
            IniciarAcabados()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaTalleres, CCTallers, CNTallers, dtIdentificadores, True)
            dtIdentificadores.Columns(CCTallers).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNTallers).Caption = rm.GetString("NOM")
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = CNTallers

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            sql = "SELECT CODI FROM TALLERS WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"
            MyBase.CambiarAReg(id, sql, accion)
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"


    Private Sub IniciarAcabados()
        Try
            Dim dt As New aura2k3.ds11.dacabatsDataTable
            acabados = New clsAcabados(dt, Me.centro, Me.bc)
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub NuevoRegistro()
        Try
            MyBase.NuevoRegistro()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CanviarAcabados()
        Try
            With acabados.dvForm
                .RowFilter = "PROVE = " & CODI & " AND CENTRO = '" & centro & "'"
                .Table.Columns("CODI").DefaultValue = ""
                .Table.Columns("PROVE").DefaultValue = CODI
                .Table.Columns("PREUM").DefaultValue = 0
                .Table.Columns("PREUK").DefaultValue = 0
                .Table.Columns("CENTRO").DefaultValue = centro
            End With

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ACN()

            MyBase.ActualizarOrigen(True)
            acabados.ActualizarOrigen(True, True)
            CCN()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Public Overloads Sub borrar()
        Try
            MyBase.borrar()
            acabados.borrar() : ActualizarOrigen()
            CanviarAcabados()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """ "

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
    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse acabados.TieneCambios Then
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
