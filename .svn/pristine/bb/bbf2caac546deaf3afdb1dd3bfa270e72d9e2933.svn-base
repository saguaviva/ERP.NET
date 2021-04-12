Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes

Public Class clsModelo
    Inherits clsADO

#Region "VARIABLES"

    Public detalleFornituras As clsDetalleForniturasModelo
    Public detalleManipulacion As clsManipulacion
    Public detalleColores As clsDetalleColoresModelo
    Public detalleStock As clsDetalleStockModelo
    Public detalleEscandallo As clsDetalleEscandalloModelo
    Public temporadasElegidas As New ArrayList 'Todas elegidas
    Public clienteElegido As Integer = -1 'Todos elegidos
    Public serieElegida As String = "" 'Todas las series elegidas
    Public dtClientesConModelos As DataTable
    Friend dtTallers As New DataTable("TALLERS")
    Friend dtForni As New DataTable("FORNI")
    Friend dtClients As New DataTable("CLIENTS")
    Friend dtTeixits As New DataTable("TEIXITS")
    Friend dtTemporadas As New DataTable("TEMPORADES")
    Friend dtProve As New DataTable("PROVE")

#End Region

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
                dvForm(pa).Row("CODI") = general.nz(mCODI, "") : GuardarDV()
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

    Private mTEIXIT As String
    Public Property TEIXIT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEIXIT = general.nz(dvForm(PA).Row("TEIXIT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEIXIT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTeixits, CCTeixits, Value) Then
                If general.nz(Value, "") <> "" Then
                    mTEIXIT = general.nz(Value, 0)
                    dvForm(PA).Row("DESCRITEIXIT") = general.OBN(Value, dtTeixits, CNTeixits)
                    dvForm(PA).Row("TEIXIT") = general.nz(Value, "") : guardarDV()
                Else
                    dvForm(PA).Row("TEIXIT") = DBNull.Value
                    dvForm(PA).Row("DESCRITEIXIT") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("TEIXIT") = 0
                dvForm(PA).Row("DESCRITEIXIT") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETEJIDO"))
            End If
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCRI = general.nz(dvForm(PA).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRI, "") Then
                mDESCRI = general.nz(Value, "")
                dvForm(PA).Row("DESCRI") = general.nz(mDESCRI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(PA).Row("PROVE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtProve, CCProve, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(PA).Row("NOMPROVE") = general.OBN(Value, dtProve, CNProve)
                    dvForm(PA).Row("PROVE") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("PROVE") = DBNull.Value

                    dvForm(PA).Row("NOMPROVE") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("PROVE") = DBNull.Value

                dvForm(PA).Row("NOMPROVE") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMPROVE = general.nz(dvForm(PA).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMPROVE = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mAMPLE As String
    Public Property AMPLE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mAMPLE = general.nz(dvForm(PA).Row("AMPLE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mAMPLE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(AMPLE, "") Then
                mAMPLE = general.nz(Value, "")
                dvForm(PA).Row("AMPLE") = general.nz(mAMPLE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTINT As Integer
    Public Property TINT() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mTINT = nzn(dvForm(PA).Row("TINT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTINT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTallers, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mTINT = nzn(Value, 0)
                    dvForm(PA).Row("NOMTINT") = general.OBN(Value, dtTallers, CNTallers)
                    dvForm(PA).Row("TINT") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("TINT") = DBNull.Value

                    dvForm(PA).Row("NOMTINT") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("TINT") = DBNull.Value

                dvForm(PA).Row("NOMTINT") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property

    Private mNOMTINT As String
    Public Property NOMTINT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMTINT = general.nz(dvForm(PA).Row("NOMTINT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTINT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMTINT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMTINT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMTINT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mACA As Integer
    Public Property ACA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mACA = nzn(dvForm(PA).Row("ACA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mACA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTallers, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mACA = nzn(Value, 0)
                    dvForm(PA).Row("NOMACA") = general.OBN(Value, dtTallers, CNTallers)
                    dvForm(PA).Row("ACA") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("ACA") = DBNull.Value

                    dvForm(PA).Row("NOMACA") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("ACA") = DBNull.Value

                dvForm(PA).Row("NOMACA") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property

    Private mNOMACA As String
    Public Property NOMACA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMACA = general.nz(dvForm(PA).Row("NOMACA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMACA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMACA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMACA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMACA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mESTAM As Integer
    Public Property ESTAM() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mESTAM = nzn(dvForm(PA).Row("ESTAM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mESTAM, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTallers, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mESTAM = nzn(Value, 0)
                    dvForm(PA).Row("NOMESTAM") = general.OBN(Value, dtTallers, CNTallers)
                    dvForm(PA).Row("ESTAM") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("ESTAM") = DBNull.Value

                    dvForm(PA).Row("NOMESTAM") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("ESTAM") = DBNull.Value

                dvForm(PA).Row("NOMESTAM") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property

    Private mNOMESTAM As String
    Public Property NOMESTAM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMESTAM = general.nz(dvForm(PA).Row("NOMESTAM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMESTAM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMESTAM = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMESTAM") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMESTAM") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mRENDIM As Double
    Public Property RENDIM() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mRENDIM = nzn(dvForm(PA).Row("RENDIM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mRENDIM, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(RENDIM, 0) Then
                mRENDIM = nzn(Value, 0)
                dvForm(PA).Row("RENDIM") = nzn(mRENDIM, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCONFEC As Double
    Public Property CONFEC() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCONFEC = nzn(dvForm(PA).Row("CONFEC"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCONFEC, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTallers, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCONFEC = nzn(Value, 0)
                    dvForm(PA).Row("NOMCONFEC") = general.OBN(Value, dtTallers, CNTallers)
                    dvForm(PA).Row("CONFEC") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("CONFEC") = DBNull.Value

                    dvForm(PA).Row("NOMCONFEC") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("CONFEC") = DBNull.Value

                dvForm(PA).Row("NOMCONFEC") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLERS"))
            End If
        End Set
    End Property

    Private mNOMCONFEC As String
    Public Property NOMCONFEC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMCONFEC = general.nz(dvForm(PA).Row("NOMCONFEC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCONFEC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMCONFEC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMCONFEC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMCONFEC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORNITURA As String
    Public Property FORNITURA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFORNITURA = general.nz(dvForm(PA).Row("FORNITURA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORNITURA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FORNITURA, "") Then
                mFORNITURA = general.nz(Value, "")
                dvForm(PA).Row("FORNITURA") = general.nz(mFORNITURA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCESTAM As String
    Public Property CESTAM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCESTAM = general.nz(dvForm(PA).Row("CESTAM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCESTAM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CESTAM, "") Then
                mCESTAM = general.nz(Value, "")
                dvForm(PA).Row("CESTAM") = general.nz(mCESTAM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCESTAM2 As String
    Public Property CESTAM2() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCESTAM2 = general.nz(dvForm(PA).Row("CESTAM2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCESTAM2, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CESTAM2, "") Then
                mCESTAM2 = general.nz(Value, "")
                dvForm(PA).Row("CESTAM2") = general.nz(mCESTAM2, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNESTAM As Double
    Public Property NESTAM() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNESTAM = nzn(dvForm(PA).Row("NESTAM"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNESTAM, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NESTAM, 0) Then
                mNESTAM = nzn(Value, 0)
                dvForm(PA).Row("NESTAM") = nzn(mNESTAM, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNESTAM2 As Double
    Public Property NESTAM2() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNESTAM2 = nzn(dvForm(PA).Row("NESTAM2"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNESTAM2, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NESTAM2, 0) Then
                mNESTAM2 = nzn(Value, 0)
                dvForm(PA).Row("NESTAM2") = nzn(mNESTAM2, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNCONFEC As Double
    Public Property NCONFEC() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNCONFEC = nzn(dvForm(PA).Row("NCONFEC"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNCONFEC, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NCONFEC, 0) Then
                mNCONFEC = nzn(Value, 0)
                dvForm(PA).Row("NCONFEC") = nzn(mNCONFEC, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNPLANXA As Double
    Public Property NPLANXA() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNPLANXA = nzn(dvForm(PA).Row("NPLANXA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNPLANXA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NPLANXA, 0) Then
                mNPLANXA = nzn(Value, 0)
                dvForm(PA).Row("NPLANXA") = nzn(mNPLANXA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNREPAS As Double
    Public Property NREPAS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNREPAS = nzn(dvForm(PA).Row("NREPAS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNREPAS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NREPAS, 0) Then
                mNREPAS = nzn(Value, 0)
                dvForm(PA).Row("NREPAS") = nzn(mNREPAS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mQTRANS As Double
    Public Property QTRANS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mQTRANS = nzn(dvForm(PA).Row("QTRANS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mQTRANS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(QTRANS, 0) Then
                mQTRANS = nzn(Value, 0)
                dvForm(PA).Row("QTRANS") = nzn(mQTRANS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNTRANS As Double
    Public Property NTRANS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNTRANS = nzn(dvForm(PA).Row("NTRANS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNTRANS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NTRANS, 0) Then
                mNTRANS = nzn(Value, 0)
                dvForm(PA).Row("NTRANS") = nzn(mNTRANS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mQFLOCAT As Double
    Public Property QFLOCAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mQFLOCAT = nzn(dvForm(PA).Row("QFLOCAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mQFLOCAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(QFLOCAT, 0) Then
                mQFLOCAT = nzn(Value, 0)
                dvForm(PA).Row("QFLOCAT") = nzn(mQFLOCAT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNFLOCAT As Double
    Public Property NFLOCAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNFLOCAT = nzn(dvForm(PA).Row("NFLOCAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNFLOCAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NFLOCAT, 0) Then
                mNFLOCAT = nzn(Value, 0)
                dvForm(PA).Row("NFLOCAT") = nzn(mNFLOCAT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mQBRODAT As Double
    Public Property QBRODAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mQBRODAT = nzn(dvForm(PA).Row("QBRODAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mQBRODAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(QBRODAT, 0) Then
                mQBRODAT = nzn(Value, 0)
                dvForm(PA).Row("QBRODAT") = nzn(mQBRODAT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNBRODAT As Double
    Public Property NBRODAT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNBRODAT = nzn(dvForm(PA).Row("NBRODAT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNBRODAT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NBRODAT, 0) Then
                mNBRODAT = nzn(Value, 0)
                dvForm(PA).Row("NBRODAT") = nzn(mNBRODAT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNESTAMP As Double
    Public Property NESTAMP() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNESTAMP = nzn(dvForm(PA).Row("NESTAMP"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNESTAMP, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NESTAMP, 0) Then
                mNESTAMP = nzn(Value, 0)
                dvForm(PA).Row("NESTAMP") = nzn(mNESTAMP, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNTINTP As Double
    Public Property NTINTP() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNTINTP = nzn(dvForm(PA).Row("NTINTP"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNTINTP, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NTINTP, 0) Then
                mNTINTP = nzn(Value, 0)
                dvForm(PA).Row("NTINTP") = nzn(mNTINTP, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNACAP As Double
    Public Property NACAP() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNACAP = nzn(dvForm(PA).Row("NACAP"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNACAP, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NACAP, 0) Then
                mNACAP = nzn(Value, 0)
                dvForm(PA).Row("NACAP") = nzn(mNACAP, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNFORNITURA As Double
    Public Property NFORNITURA() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNFORNITURA = nzn(dvForm(PA).Row("NFORNITURA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNFORNITURA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NFORNITURA, 0) Then
                mNFORNITURA = nzn(Value, 0)
                dvForm(PA).Row("NFORNITURA") = nzn(mNFORNITURA, 0) : guardarDV()
            End If
        End Set
    End Property
    Private mMANIPULACION As Double
    Public Property MANIPULACION() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mMANIPULACION = nzn(dvForm(PA).Row("MANIPULACION"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMANIPULACION, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(MANIPULACION, 0) Then
                mMANIPULACION = nzn(Value, 0)
                dvForm(PA).Row("MANIPULACION") = nzn(mMANIPULACION, 0) : guardarDV()
            End If
        End Set
    End Property
    Private mCOST As Double
    Public Property COST() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOST = nzn(dvForm(PA).Row("COST"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOST, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COST, 0) Then
                mCOST = nzn(Value, 0)
                dvForm(PA).Row("COST") = nzn(mCOST, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mMARGE As Double
    Public Property MARGE() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mMARGE = nzn(dvForm(PA).Row("MARGE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mMARGE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(MARGE, 0) Then
                mMARGE = nzn(Value, 0)
                dvForm(PA).Row("MARGE") = nzn(mMARGE, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mVENDA As Double
    Public Property VENDA() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mVENDA = nzn(dvForm(PA).Row("VENDA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mVENDA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(VENDA, 0) Then
                mVENDA = nzn(Value, 0)
                dvForm(PA).Row("VENDA") = nzn(mVENDA, 0) : guardarDV()
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

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mIVA = general.nz(dvForm(PA).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IVA, "") Then
                mIVA = general.nz(Value, "")
                dvForm(PA).Row("IVA") = general.nz(mIVA, "") : guardarDV()
            End If
        End Set
    End Property
    Private mTALLA01 As String
    Public Property TALLA01() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA01 = general.nz(dvForm(PA).Row("TALLA01"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA01, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA01, "") Then
                mTALLA01 = general.nz(Value, "")
                dvForm(PA).Row("TALLA01") = general.nz(mTALLA01, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA02 As String
    Public Property TALLA02() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA02 = general.nz(dvForm(PA).Row("TALLA02"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA02, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA02, "") Then
                mTALLA02 = general.nz(Value, "")
                dvForm(PA).Row("TALLA02") = general.nz(mTALLA02, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA03 As String
    Public Property TALLA03() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA03 = general.nz(dvForm(PA).Row("TALLA03"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA03, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA03, "") Then
                mTALLA03 = general.nz(Value, "")
                dvForm(PA).Row("TALLA03") = general.nz(mTALLA03, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA04 As String
    Public Property TALLA04() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA04 = general.nz(dvForm(PA).Row("TALLA04"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA04, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA04, "") Then
                mTALLA04 = general.nz(Value, "")
                dvForm(PA).Row("TALLA04") = general.nz(mTALLA04, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA05 As String
    Public Property TALLA05() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA05 = general.nz(dvForm(PA).Row("TALLA05"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA05, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA05, "") Then
                mTALLA05 = general.nz(Value, "")
                dvForm(PA).Row("TALLA05") = general.nz(mTALLA05, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA06 As String
    Public Property TALLA06() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA06 = general.nz(dvForm(PA).Row("TALLA06"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA06, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA06, "") Then
                mTALLA06 = general.nz(Value, "")
                dvForm(PA).Row("TALLA06") = general.nz(mTALLA06, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA07 As String
    Public Property TALLA07() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA07 = general.nz(dvForm(PA).Row("TALLA07"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA07, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA07, "") Then
                mTALLA07 = general.nz(Value, "")
                dvForm(PA).Row("TALLA07") = general.nz(mTALLA07, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA08 As String
    Public Property TALLA08() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA08 = general.nz(dvForm(PA).Row("TALLA08"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA08, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA08, "") Then
                mTALLA08 = general.nz(Value, "")
                dvForm(PA).Row("TALLA08") = general.nz(mTALLA08, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA09 As String
    Public Property TALLA09() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA09 = general.nz(dvForm(PA).Row("TALLA09"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA09, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA09, "") Then
                mTALLA09 = general.nz(Value, "")
                dvForm(PA).Row("TALLA09") = general.nz(mTALLA09, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA10 As String
    Public Property TALLA10() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA10 = general.nz(dvForm(PA).Row("TALLA10"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA10, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA10, "") Then
                mTALLA10 = general.nz(Value, "")
                dvForm(PA).Row("TALLA10") = general.nz(mTALLA10, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNPACK As Double
    Public Property NPACK() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mNPACK = nzn(dvForm(PA).Row("NPACK"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNPACK, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NPACK, 0) Then
                mNPACK = nzn(Value, 0)
                dvForm(PA).Row("NPACK") = nzn(mNPACK, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCODIMODEL As String
    Public Property CODIMODEL() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCODIMODEL = general.nz(dvForm(PA).Row("CODIMODEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCODIMODEL, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CODIMODEL, "") Then
                mCODIMODEL = general.nz(Value, "")
                dvForm(PA).Row("CODIMODEL") = general.nz(mCODIMODEL, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDESCRITEIXIT As String
    Public Property DESCRITEIXIT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCRITEIXIT = general.nz(dvForm(PA).Row("DESCRITEIXIT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRITEIXIT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mDESCRITEIXIT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DESCRITEIXIT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DESCRITEIXIT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext, "CODI")
        Dim sqlSel As String
        Try
            ACN()
            'GENERICO
            sqlSinWhere = "SELECT models.* FROM models "

            sqlSel = sqlSinWhere & _
                        genWhere() & " LIMIT 0,1"

            numeroRegistros = obtenerNumeroReg(tablaModelos, "")
            numeroRegistroActual = 0
            ''Con esto seleccionamos las temporadas que queremos mostrar en la tabla modelos

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)

            dvForm = tabla.DefaultView
            dvForm.Sort = "TEMPORADA, CLIENT, CODI, SERIE"

            'PARA LOS TEJIDOS
            IniciarDetalles()
            CargarTablas()

            CargarIdentificadores()
            PonerNombres()
            PonerFiltroIdentificadores()
            DespertarHandlers()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            Dim a As New ArrayList
            Dim i As Integer

            a.Add(CCModelo)
            a.Add(CNModelo)
            a.Add("CLIENT")
            a.Add("TEMPORADA")
            a.Add("SERIE")

            CargaTabla(tablaModelos, a, dtIdentificadores, True)

            If Not dtIdentificadores.Columns.Contains("NOMCLIENT") Then
                dtIdentificadores.Columns.Add("NOMCLIENT")
            End If

            'Los strings que se vean correctamte en el cboid
            dtIdentificadores.Columns("NOMCLIENT").Caption = rm.GetString("NOMCLIENT")
            dtIdentificadores.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns("DESCRI").Caption = rm.GetString("DESCRIPCION")
            dtIdentificadores.Columns("SERIE").Caption = rm.GetString("SERIE")
            dtIdentificadores.Columns("TEMPORADA").Caption = rm.GetString("TEMPORADA")
            dtIdentificadores.Columns("CLIENT").Caption = rm.GetString("CLIENTE")

            'Le ponemos los nombres a los proveedores
            For i = 0 To dtIdentificadores.Rows.Count - 1
                dtIdentificadores.Rows(i).Item("NOMCLIENT") = general.OBN(dtIdentificadores.Rows(i).Item("CLIENT"), dtClients)
            Next

            'Orden de los identificadores
            dvIdentificadores.Sort = CCMuestra
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overrides Sub SiguienteReg()
        Try
            DormirHandlers()
            CargarRegistro(siguiente, IIf(Not HaySeleccion(), -1, CODI))
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            DormirHandlers()
            CargarRegistro(anterior, IIf(Not HaySeleccion(), -1, CODI))
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            DormirHandlers()
            Me.CargarRegistro(ultimo)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            DormirHandlers()
            Me.CargarRegistro(primero)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal dr As Object, ByVal accion As Object)
        Dim SQL As String
        Try
            DormirHandlers()
            If accion = iraRegistroFila Then
                MyBase.CambiarAReg(dr, _
                    "SELECT * FROM MODELS WHERE " & _
                    "MODELS.CENTRO =  """ & dr("centro") & """ AND " & _
                    "CODI = """ & dr("codi") & """  AND " & _
                    "TEMPORADA = """ & dr("TEMPORADA") & """  AND " & _
                    "SERIE = """ & dr("SERIE") & """  AND " & _
                    "CLIENT = " & dr("CLIENT"), iraRegistroFila)
            Else
                MyBase.CambiarAReg(dr, "", accion)
            End If
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "CARGA TABLAS"

    Private Sub CargarTablas()
        Try
            CargaTabla(tablaProveedores, CCProve, CNProve, dtProve, True)
            CargaTabla(tablaTalleres, CCTallers, CNTallers, dtTallers, True)
            CargaTabla(tablaClientes, CCClients, CNClients, dtClients, True)
            CargaTabla(tablaTemporadas, "CODI", "DESCRI", dtTemporadas, True)
            CargaTablaFornituras(True)
            CargaTablaTejidos(True)
            'CargaTabla("IVA", "CODI", "DESCRIPCIO", dtIVA, True, centro)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaTablaFornituras(ByVal NOCERRAR As Boolean)
        '!!!!! CARGA SOLO LAS FORNITURAS DEL CENTRO ACTUAL
        Dim cmdSelect As New MySqlCommand
        Dim daSelect As New MySqlDataAdapter
        Try
            cmdSelect.Connection = cnn
            cmdSelect.CommandText = "SELECT CODI, " & _
                        " DESCRI, PREU " & _
                        " FROM FORNI  " & _
                        " WHERE CENTRO = """ & centro & """ " & _
                        " ORDER BY CODI"
            daSelect.SelectCommand = cmdSelect
            ACN()
            dtForni.Rows.Clear()
            daSelect.Fill(dtForni)

            dtForni.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtForni.Columns("DESCRI").Caption = rm.GetString("DESCRIPCION")
            dtForni.Columns("PREU").Caption = rm.GetString("PRECIO")

            dtForni.DefaultView.Sort = "CODI"

            If Not cnn.State = ConnectionState.Closed AndAlso Not NOCERRAR Then
                cnn.Close()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaTablaTejidos(ByVal NOCERRAR As Boolean)
        Dim cmdSelect As New MySqlCommand
        Dim daSelect As New MySqlDataAdapter
        Try

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = "SELECT CODI, " & _
                        " DESCRI, PREUPERMODEL AS PREU " & _
                        " FROM TEIXITS  " & _
                        " WHERE CENTRO = """ & centro & """ " & _
                        " ORDER BY CODI"
            daSelect.SelectCommand = cmdSelect
            ACN()
            dtTeixits.Rows.Clear()
            daSelect.Fill(dtTeixits)

            dtTeixits.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtTeixits.Columns("DESCRI").Caption = rm.GetString("DESCRIPCION")
            dtTeixits.Columns("PREU").Caption = rm.GetString("PRECIO")

            dtTeixits.DefaultView.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Public Sub CargarClientesConModelos()
        Try
            Dim cmd As New MySqlCommand("SELECT DISTINCT CLIENTS.CODI, CLIENTS.NOM FROM CLIENTS " & _
                            " INNER JOIN MODELS ON (MODELS.CLIENT = CLIENTS.CODI) " & _
                            " " & genWhereSeleccion("MODELS.") & " ", cnn)

            Dim dar As MySqlDataReader
            Dim dr As DataRow
            Dim dc1 As New DataColumn("CODI")
            Dim dc2 As New DataColumn("NOM")
            cnn.Open()
            dar = cmd.ExecuteReader
            dtClientesConModelos = New DataTable
            dtClientesConModelos.Columns.Clear()
            dtClientesConModelos.Columns.Add(dc1)
            dtClientesConModelos.Columns.Add(dc2)

            While dar.Read
                dr = dtClientesConModelos.NewRow
                dr("CODI") = dar("CODI")
                dr("NOM") = dar("NOM")
                dtClientesConModelos.Rows.Add(dr)
            End While
            dar.Close()
            cnn.Close()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub DespertarHandlers()
        Try
            AddHandler tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CambioColumna)
            AddHandler detalleFornituras.tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CambioColumna)
            AddHandler detalleEscandallo.tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CambioColumna)
            AddHandler detalleColores.tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CambioColumna)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub DormirHandlers()
        Try
            RemoveHandler tabla.ColumnChanged, AddressOf CambioColumna
            RemoveHandler detalleFornituras.tabla.ColumnChanged, AddressOf CambioColumna
            RemoveHandler detalleEscandallo.tabla.ColumnChanged, AddressOf CambioColumna
            RemoveHandler detalleColores.tabla.ColumnChanged, AddressOf CambioColumna

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarDetalles()
        Dim dt As DataTable
        Try
            dt = New aura2k3.ds11.mforniDataTable
            detalleFornituras = New clsDetalleForniturasModelo(dt, centro, bc, Me)

            dt = New aura2k3.ds11.modelsescandalloDataTable
            detalleEscandallo = New clsDetalleEscandalloModelo(dt, centro, bc, Me)

            dt = New aura2k3.ds11.modcolDataTable
            detalleColores = New clsDetalleColoresModelo(dt, centro, bc, Me)

            dt = New aura2k3.ds11.modstkDataTable
            detalleStock = New clsDetalleStockModelo(dt, centro, bc, Me)

            dt = New aura2k3.ds11.mmaniDataTable
            detalleManipulacion = New clsManipulacion(dt, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ElegirTemporadas(ByVal aTemporadas As ArrayList)
        Try
            If aTemporadas Is Nothing Then
                'Tenemos todas las temporadas elegidas
                temporadasElegidas.Clear()
                numeroRegistros = obtenerNumeroReg(t, "")
                CargarRegistro(primero)
            Else
                temporadasElegidas = aTemporadas
                numeroRegistros = obtenerNumeroReg(t, "")
                CargarRegistro(primero, esCambioSeleccion)
            End If
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ElegirCliente(ByVal cliente As Integer)
        Try
            clienteElegido = cliente
            numeroRegistros = obtenerNumeroReg(t, "")
            If cliente = -1 Then : CargarRegistro(primero)
            Else : CargarRegistro(primero, esCambioSeleccion) : End If
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    'Obtiene una imagen de la base de datos para el modelo
    'Guardamos aqui la SQL
    Public Function obtenerImagen(ByVal codigoImagen As String) As Object
        Dim strSQL As String
        Dim obj As New Object
        Dim tmp As Integer
        Try
            CCN()
            Dim cmdSel As MySqlCommand
            tmp = cnn.ConnectionTimeout
            'REVISAR
            'cnn.ConnectionTimeout = 360000

            strSQL = "SELECT FOTO FROM MODELSFOTOS WHERE MODEL = """ & general.NS(CODI) & """ AND " & _
                        " SERIE = """ & general.NS(SERIE) & """ AND " & _
                        " CLIENT = """ & general.NS(CLIENT) & """ AND " & _
                        " TEMPORADA = """ & general.NS(TEMPORADA) & """ AND " & _
                        " CENTRO = """ & centro & """ AND " & _
                        " CODIGOIMAGEN = """ & codigoImagen & """ "
            cmdSel = New MySqlCommand(strSQL, cnn)
            ACN()
            obj = cmdSel.ExecuteScalar
            CCN()
            'REVISAR
            'cnn.ConnectionTimeout = tmp
            Return obj

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    'Obtiene una imagen de la base de datos para el modelo
    'Guardamos aqui la SQL
    Public Sub guardarImagen(ByVal codigoImagen As String)
        Dim Foto() As Byte
        Dim NavArchivos As System.Windows.Forms.OpenFileDialog
        Dim cmdIns As New MySqlCommand
        Dim Fs As System.IO.FileStream
        Dim cmdDel As New MySqlCommand
        Try

            cmdIns.CommandText = "INSERT INTO MODELSFOTOS (MODEL, SERIE, TEMPORADA, CLIENT, FOTO, CENTRO, CODIGOIMAGEN) VALUES (" & _
                    " """ & general.NS(CODI) & """, " & _
                    " """ & general.NS(SERIE) & """, " & _
                    " """ & general.NS(TEMPORADA) & """, " & _
                    " """ & CLIENT & """, " & _
                    " @pFOTO, " & _
                    " """ & centro & """, " & _
                    " """ & codigoImagen & """ )"

            cmdIns.Connection = cnn
            'Añadimos este parametro para poder actualizar la BD
            cmdIns.Parameters.Add(New MySqlParameter("pFOTO", MySqlDbType.Blob, 0, "FOTO"))

            NavArchivos = New System.Windows.Forms.OpenFileDialog
            NavArchivos.Filter = "Imatges Suportades (*.jpeg;*.jpg;*.bmp;)|*.jpeg;*.jpg;*.bmp"
            NavArchivos.ShowDialog()
            Fs = New System.IO.FileStream(NavArchivos.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            Foto = New Byte(Fs.Length) {}
            Fs.Read(Foto, 0, Fs.Length)
            Fs.Close()

            ACN()
            'La borramos de la base de datos
            cmdDel.CommandText = "DELETE FROM MODELSFOTOS WHERE " & _
                        " MODEL = """ & general.NS(CODI) & """ AND " & _
                        " SERIE = """ & general.NS(SERIE) & """ AND " & _
                        " CLIENT = """ & general.NS(CLIENT) & """ AND " & _
                        " TEMPORADA = """ & general.NS(TEMPORADA) & """ AND " & _
                        " CENTRO = """ & centro & """ AND " & _
                        " CODIGOIMAGEN = """ & codigoImagen & """ "

            cmdDel.Connection = cnn

            cmdDel.ExecuteNonQuery()
            'Introducimos la nueva en la base de datos
            cmdIns.Parameters("pFOTO").Value = Foto
            cmdIns.ExecuteNonQuery()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : Fs.Close() : CCN()

        End Try
    End Sub
    Public Sub BorrarImagen(ByVal codigoImagen As String)
        Dim cmdDel As New MySqlCommand
        Try
            cmdDel.CommandText = "DELETE FROM MODELSFOTOS WHERE " & _
                                   " MODEL = """ & general.NS(CODI) & """ AND " & _
                                   " SERIE = """ & general.NS(SERIE) & """ AND " & _
                                   " CLIENT = """ & general.NS(CLIENT) & """ AND " & _
                                   " TEMPORADA = """ & general.NS(TEMPORADA) & """ AND " & _
                                   " CENTRO = """ & centro & """ AND " & _
                                   " CODIGOIMAGEN = """ & codigoImagen & """ "

            cmdDel.Connection = cnn
            ACN()
            cmdDel.ExecuteNonQuery()
            CCN()

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
            NOMTINT = general.OBN(TINT, dtTallers)
            NOMCONFEC = general.OBN(CONFEC, dtTallers)
            NOMACA = general.OBN(ACA, dtTallers)
            NOMESTAM = general.OBN(ESTAM, dtTallers)
            DESCRITEIXIT = general.OBN(TEIXIT, dtTeixits, "DESCRI")

            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try

    End Sub

#End Region

#Region "MODIFICAR ORIGEN"

    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow

        Try
            ActualizarOrigen()
            tabla.Clear()
            drNew = tabla.NewRow()

            drNew.Item("CODI") = ""
            drNew.Item("SERIE") = ""
            drNew.Item("CLIENT") = 0
            drNew.Item("TEMPORADA") = ""
            drNew.Item("CENTRO") = centro

            tabla.Rows.Add(drNew)
            numeroRegistros = numeroRegistros + 1

            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Dim cambios As Boolean = False
        Try
            If Not CODI = "" Or TEMPORADA = "" Or CLIENT = 0 Then

                MyBase.ActualizarOrigen(True)

                detalleFornituras.ActualizarOrigen(True, True)
                detalleColores.ActualizarOrigen(True, True)
                detalleEscandallo.ActualizarOrigen(True, True)
                detalleStock.ActualizarOrigen(True, True)
                detalleManipulacion.ActualizarOrigen(True, True)
                MoverActual()
                CCN()
            Else
                If (CODI = "") Then MessageBox.Show(rm.GetString("CODIGOMODELOINCO"), "Error", MessageBoxButtons.OK)
                If (TEMPORADA = "") Then MessageBox.Show(rm.GetString("CODIGOTEMPINCO"), "Error", MessageBoxButtons.OK)
                If (CLIENT = 0) Then MessageBox.Show(rm.GetString("SEHAESCOGERCLIENTE"), "Error", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub borrar()
        Dim cambios As Boolean
        Try
            DormirHandlers()
            ACN()

            detalleFornituras.borrar() ': ActualizarOrigen()
            detalleColores.borrar() ': ActualizarOrigen()
            detalleEscandallo.borrar() ': ActualizarOrigen()
            detalleManipulacion.borrar() ': ActualizarOrigen()
            detalleStock.borrar() ': ActualizarOrigen()

            'BorrarActualDV(True)
            MyBase.borrar()
            CargarRegistro(siguiente)

            numeroRegistroActual = ObtenerNumeroRegistro(CODI)
            numeroRegistros = numeroRegistros - 1

            MoverActual()
            CCN()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "GENSQLS"

    Private Function obtenerRowFilter() As String
        Return "MODEL = '" & CODI & "' AND SERIE = '" & SERIE & "' AND TEMPORADA = '" & TEMPORADA & "' AND CLIENT = '" & CLIENT & "' "
    End Function

#End Region

#Region "CALCULOS"

    Private Function tieneCambiosColores() As Boolean
        Try
            Return Not detalleColores.tabla.GetChanges Is Nothing
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function tieneCambiosEscandallo() As Boolean
        Try
            Return Not detalleEscandallo.tabla.GetChanges Is Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function tieneCambiosFornituras() As Boolean
        Try
            Return Not detalleFornituras.tabla.GetChanges Is Nothing
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Sub HacerCalculos()
        Dim margen As Double
        Try
            DormirHandlers()
            With dvForm
                If tieneCambiosEscandallo() Then
                    detalleEscandallo.COST = roundnum(detalleEscandallo.PREU * detalleEscandallo.CONSUM, 2)
                End If
                'If tieneCambiosFornituras() Then
                detalleFornituras.IMPORT = roundnum(detalleFornituras.PREU * detalleFornituras.UNITATS, 2)
                ' End If
                If tieneCambiosColores() Or tieneCambiosEscandallo() Then
                    'detalleColores.ModificarColores()
                End If
                If algunoTieneCambios() Then
                    If NFORNITURA <> 0 Then
                        If roundnum(sumaTotal("IMPORT", detalleFornituras.dvForm), 2) <> 0 Then
                            NFORNITURA = roundnum(sumaTotal("IMPORT", detalleFornituras.dvForm), 2)
                        End If
                    Else
                        NFORNITURA = roundnum(sumaTotal("IMPORT", detalleFornituras.dvForm), 2)
                    End If
                    MANIPULACION = roundnum(sumaTotal("PREU", detalleManipulacion.dvForm), 2)
                    'NREPAS es tall
                    COST = roundnum(NCONFEC + NPLANXA + NREPAS + NPACK + NFORNITURA + MANIPULACION + sumaTotal("COST", detalleEscandallo.dvForm), 2)
                    margen = MARGE / 100
                    VENDA = roundnum(COST + margen * COST, 2)
                End If
            End With
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CambioColumna(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

        Try
            'DormirHandlers()
            If (Not sender Is Nothing) Then
                Select Case DirectCast(sender, DataTable).TableName
                    Case tabla.TableName : guardarDV()
                    Case detalleColores.tabla.TableName
                        detalleColores.guardarDV()

                    Case detalleEscandallo.tabla.TableName : detalleEscandallo.guardarDV()
                    Case detalleFornituras.tabla.TableName : detalleFornituras.guardarDV()
                    Case detalleStock.tabla.TableName : detalleStock.guardarDV()
                End Select

                If (Not sender.getChanges Is Nothing) Then
                    If Not PA() = -1 Then
                        HacerCalculos()
                    End If
                End If
            End If
            'DespertarHandlers()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function forniturasTieneCambios() As Boolean

    End Function
    Private Function tieneCambiosManipulacion()
        Try
            Return Not detalleManipulacion.tabla.GetChanges Is Nothing

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function algunoTieneCambios() As Boolean
        Try
            If tieneCambiosManipulacion OrElse tieneCambiosEscandallo() OrElse tieneCambiosFornituras() OrElse Not tabla.GetChanges Is Nothing Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

#Region "CAMBIO"

    Private Sub PonerFiltroIdentificadores()
        Dim filter As String = ""
        Dim i As Integer
        Try
            If temporadasElegidas.Count <> 0 Then
                filter = " (TEMPORADA = '" & temporadasElegidas(i) & "' "
                For i = 1 To temporadasElegidas.Count - 1
                    filter = filter & " OR TEMPORADA = '" & temporadasElegidas(i) & "' "
                Next
                filter = filter & " ) "
            End If

            If clienteElegido <> -1 Then
                If filter <> "" Then
                    filter = filter & " AND CLIENT = '" & clienteElegido & "' "
                Else
                    filter = "CLIENT = '" & clienteElegido & "' "
                End If
            End If

            dvIdentificadores.RowFilter = filter & IIf(filter = "", " CENTRO = '" & centro & "' ", " AND CENTRO = '" & centro & "' ")

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            'CanviarCartaDeFornituras()
            detalleFornituras.CambioDetalle(centro, Me)
            'CanviarEscandallo()
            detalleEscandallo.CambioDetalle(centro, Me)
            'CanviarColores()
            detalleColores.CambioDetalle(centro, Me)
            'CanviarStock()
            detalleStock.CambioDetalle(centro, Me)
            detalleManipulacion.CambioDetalle(centro, Me)

            PonerNombres()
            PonerFiltroIdentificadores()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse detalleColores.TieneCambios OrElse detalleEscandallo.TieneCambios OrElse detalleFornituras.TieneCambios OrElse detalleManipulacion.TieneCambios OrElse detalleStock.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function GENWHERECONTODO(ByVal id As Object) As String
        Try
            If id Is Nothing Then
                Return " WHERE CODI = """ & CODI & """ AND TEMPORADA = """ & TEMPORADA & """ AND CLIENT = " & CLIENT & " AND SERIE = """ & SERIE & """"
            Else
                Return " WHERE CODI = """ & id & """ "
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSeleccion(ByVal tabla As String) As String
        Try
            Dim ret As String
            Dim i As Integer
            ret = "WHERE " & tabla & "CENTRO = """ & centro & """ "
            If temporadasElegidas.Count <> 0 Then
                ret = ret
                For i = 0 To temporadasElegidas.Count - 1
                    If i = 0 Then
                        ret = ret & " AND (" & tabla & "TEMPORADA = """ & temporadasElegidas(i) & """ "
                    Else
                        ret = ret & " OR " & tabla & "TEMPORADA = """ & temporadasElegidas(i) & """ "
                    End If
                Next
                ret = ret & " ) "
            End If
            If clienteElegido <> -1 Then
                ret = ret & " AND " & tabla & "CLIENT = " & clienteElegido & " "
            End If

            If serieElegida <> "" Then
                ret = ret & " AND " & tabla & "SERIE = """ & SERIE & """ "
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " where " & WCNoTabla()
            Else
                ret = genWhereSeleccion("")
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        Try
            If HaySeleccion() Then
                ret = genWhereSeleccion(t() & ".")
            Else
                ret = " WHERE " & WC() & " "
            End If

            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer

        Dim idx As Object
        Dim cmd As MySqlCommand
        Dim esCambio As Boolean = False
        Try
            If id Is Nothing Then : id = CODI : Else
                If id.GetType Is GetType(Integer) Then
                    If id = esCambioSeleccion Then : esCambio = True : End If
                End If
            End If

            cmd = New MySqlCommand(" SELECT (SELECT COUNT(*) " & _
               " FROM " & t() & " AS M2 " & genWhereSubSelect(id, esCambio) & " " & _
               " M2.TEMPORADA < M1.TEMPORADA " & _
               genWhereSubSeleccion() & ") AS rownum FROM " & t() & " AS M1 " & _
               " " & GENWHERECONTODO(id, esCambio) & "  AND " & WCNoTabla() & GenOrder(), cnn)
            ACN()
            idx = nzn(cmd.ExecuteScalar, 0)
            If idx Is Nothing Then Return -1
            Return idx

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function HaySeleccion() As Boolean
        Try
            If temporadasElegidas IsNot Nothing

          
            If temporadasElegidas.Count <> 0 Or _
                    (clienteElegido <> -1 And _
                    clienteElegido <> 0) Or _
                    serieElegida <> "" Then

                Return True

            End If
                  End If
            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function GENWHERECONTODO(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try
            If esUnaDatarow(id) Then
                'If Not HaySeleccion() Then
                '    ret = " WHERE CODI = """ & id("CODI") & """ AND CLIENT = " & id("CLIENT") & " AND " & WCNoTabla() & " "
                'Else
                '    If id Is Nothing Or esCambio Then
                '        ret = " WHERE  " & WCNoTabla() & " AND CLIENT = """ & id("CLIENT") & """ "
                '    Else
                '        ret = " WHERE  " & WCNoTabla() & " AND CODI = """ & id("CODI") & """ AND CLIENT = """ & id("CLIENT") & """ "
                '    End If
                'End If
                If Not HaySeleccion() Then
                    ret = " WHERE CODI = """ & id("CODI") & """ AND " & _
                            " SERIE = """ & id("SERIE") & """ AND " & _
                            " TEMPORADA = """ & id("TEMPORADA") & """ AND " & _
                            " CLIENT = " & id("CLIENT") & " AND " & WCNoTabla() & " "
                Else
                    If id Is Nothing Or esCambio Then
                        ret = genWhereSeleccion("")
                    Else
                        ret = genWhereSeleccion("") & " AND CODI = """ & id("CODI") & """ "
                    End If
                End If
                Return ret
            Else
                'lo de siempre
                If Not HaySeleccion() Then
                    ret = " WHERE CODI = """ & id & """ AND " & WCNoTabla() & " "
                Else
                    If id Is Nothing Or esCambio Then
                        ret = genWhereSeleccion("")
                    Else
                        ret = genWhereSeleccion("") & " AND CODI = """ & id & """ "
                    End If
                End If
                Return ret
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSubSelect(ByVal id As Object, ByVal esCambio As Boolean) As String
        Try
            '!!!!!!!!!!!!
            If esUnaDatarow(id) Then
                If esCambio Then
                    Return genWhereSeleccion("M1.") ' " M1.CLIENT = " & id("CLIENT") & " AND "
                End If

                If HaySeleccion() Then
                    Return genWhereSeleccion("") & " AND " ' " CLIENT = " & id("CLIENT") & " AND "
                Else
                    Return " WHERE "
                End If
            Else
                If esCambio Then
                    Return genWhereSeleccion("M1.") & " AND "
                End If

                If HaySeleccion() Then
                    Return genWhereSeleccion("") & " AND "
                Else
                    Return " WHERE "
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSubSeleccion()
        If HaySeleccion() Then
            Return " OR M2.TEMPORADA = M1.TEMPORADA AND M2.CLIENT < M1.CLIENT "
        Else
            'Hay que acabarlo
            Return " OR M2.TEMPORADA = M1.TEMPORADA AND M2.CLIENT < M1.CLIENT "

        End If
    End Function

#End Region

#Region "CAMBIO CENTRO"

    Public Overloads Function cambioCentro(ByVal centro As Char, ByVal iralregistro As Integer) As Boolean
        Try
            MyBase.cambioCentro(centro, iralregistro)
            MoverActual()
            CargaTablaFornituras(True)
            CargaTablaTejidos(True)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

End Class
