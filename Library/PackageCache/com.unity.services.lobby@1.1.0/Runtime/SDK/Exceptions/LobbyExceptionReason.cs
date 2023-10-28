namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Enumerates the known error causes when communicating with the Lobby Service.
    /// N.B. Error code range for this service: 16000-16999
    /// </summary>
    public enum LobbyExceptionReason
    {
        #region Lobby Errors
        /// <summary>
        /// The returned value could not be parsed, such as an error code was not included in the response.
        /// </summary>
        UnknownErrorCode = 0,

        /// <summary>
        /// Validation check failed on Lobby e.g. in the case of a failed player id match.
        /// </summary>
        ValidationError = 16000,

        /// <summary>
        /// Lobby with the given ID was not found or has already ended.
        /// </summary>
        LobbyNotFound = 16001,

        /// <summary>
        /// Player data with the given ID was not found in the specified Lobby.
        /// </summary>
        PlayerNotFound = 16002,

        /// <summary>
        /// There was a resource conflict when attempting to access Lobby data.
        /// Potentially caused by asynchronous access to resources.
        /// </summary>
        LobbyConflict = 16003,

        /// <summary>
        /// Target Lobby already has the maximum number of players.
        /// No additional members can be added.
        /// </summary>
        LobbyFull = 16004,

        /// <summary>
        /// Target Lobby is locked.
        /// No additional players can join.
        /// </summary>
        LobbyLocked = 16005,

        /// <summary>
        /// No accessible lobbies are currently available for quick-join.
        /// </summary>
        NoOpenLobbies = 16006,

        /// The Lobby cannot be created because it already exists.
        /// </summary>
        LobbyAlreadyExists = 16007,

        /// <summary>
        /// Player attempted to join a lobby with a password that did not match the lobby's password.
        /// </summary>
        IncorrectPassword = 16009,

        /// <summary>
        /// Player attempted to join a lobby with an invalid join code (e.g. it contained invalid characters).
        /// </summary>
        InvalidJoinCode = 16010,
        #endregion

        #region Http Errors
        /// <summary>
        /// Error code representing HTTP Status Code of 400 for the Lobby Service.
        /// The request could not be understood by the server due to malformed syntax.
        /// </summary>
        InvalidArgument = 16400,

        /// <summary>
        /// Error code representing HTTP Status Code of 400 for the Lobby Service.
        /// The request made was invalid and will not be processed by the service.
        /// </summary>
        BadRequest = 16400,

        /// <summary>
        /// Error code representing HTTP Status Code of 401 for the Lobby Service.
        /// The request requires authentication.
        /// </summary>
        Unauthorized = 16401,

        /// <summary>
        /// Error code representing HTTP Status Code of 402 for the Lobby Service.
        /// This error code is reserved for future use.
        /// </summary>
        PaymentRequired = 16402,

        /// <summary>
        /// Error code representing HTTP Status Code of 403 for the Lobby Service.
        /// The server understood the request, and refuses to fulfill it.
        /// </summary>
        Forbidden = 16403,

        /// <summary>
        /// Error code representing HTTP Status Code of 404 for the Lobby Service.
        /// The server has not found the specified resource.
        /// </summary>
        EntityNotFound = 16404,

        /// <summary>
        /// Error code representing HTTP Status Code of 405 for the Lobby Service.
        /// The method specified is not allowed for the specified resource.
        /// </summary>
        MethodNotAllowed = 16405,

        /// <summary>
        /// Error code representing HTTP Status Code of 406 for the Lobby Service.
        /// The server cannot provide a response that matches the acceptable values for the request.
        /// </summary>
        NotAcceptable = 16406,

        /// <summary>
        /// Error code representing HTTP Status Code of 407 for the Lobby Service.
        /// The request requires authentication with the proxy.
        /// </summary>
        ProxyAuthenticationRequired = 16407,

        /// <summary>
        /// Error code representing HTTP Status Code of 408 for the Lobby Service.
        /// The request was not made within the time the server was prepared to wait.
        /// </summary>
        RequestTimeOut = 16408,

        /// <summary>
        /// Error code representing HTTP Status Code of 409 for the Lobby Service.
        /// The request could not be completed due to a conflict with the current state on the server.
        /// </summary>
        Conflict = 16409,

        /// <summary>
        /// Error code representing HTTP Status Code of 410 for the Lobby Service.
        /// The requested resource is no longer available and there is no known forwarding address.
        /// </summary>
        Gone = 16410,

        /// <summary>
        /// Error code representing HTTP Status Code of 411 for the Lobby Service.
        /// The server refuses to accept the request without a defined content-length.
        /// </summary>
        LengthRequired = 16411,

        /// <summary>
        /// Error code representing HTTP Status Code of 412 for the Lobby Service.
        /// A precondition given in the request was not met when tested on the server.
        /// </summary>
        PreconditionFailed = 16412,

        /// <summary>
        /// Error code representing HTTP Status Code of 413 for the Lobby Service.
        /// The request entity is larger than the server is willing or able to process.
        /// </summary>
        RequestEntityTooLarge = 16413,

        /// <summary>
        /// Error code representing HTTP Status Code of 414 for the Lobby Service.
        /// The request URI is longer than the server is willing to interpret.
        /// </summary>
        RequestUriTooLong = 16414,

        /// <summary>
        /// Error code representing HTTP Status Code of 415 for the Lobby Service.
        /// The request is in a format not supported by the requested resource for the requested method.
        /// </summary>
        UnsupportedMediaType = 16415,

        /// <summary>
        /// Error code representing HTTP Status Code of 416 for the Lobby Service.
        /// The requested ranges cannot be served.
        /// </summary>
        RangeNotSatisfiable = 16416,

        /// <summary>
        /// Error code representing HTTP Status Code of 417 for the Lobby Service.
        /// An expectation in the request cannot be met by the server.
        /// </summary>
        ExpectationFailed = 16417,

        /// <summary>
        /// Error code representing HTTP Status Code of 418 for the Lobby Service.
        /// The server refuses to brew coffee because it is, permanently, a teapot. Defined by the Hyper Text Coffee Pot Control Protocol defined in April Fools' jokes in 1998 and 2014.
        /// </summary>
        Teapot = 16418,

        /// <summary>
        /// Error code representing HTTP Status Code of 421 for the Lobby Service.
        /// The request was directed to a server that is not able to produce a response.
        /// </summary>
        Misdirected = 16421,

        /// <summary>
        /// Error code representing HTTP Status Code of 422 for the Lobby Service.
        /// The request is understood, but the server was unable to process its instructions.
        /// </summary>
        UnprocessableTransaction = 16422,

        /// <summary>
        /// Error code representing HTTP Status Code of 423 for the Lobby Service.
        /// The source or destination resource is locked.
        /// </summary>
        Locked = 16423,

        /// <summary>
        /// Error code representing HTTP Status Code of 424 for the Lobby Service.
        /// The method could not be performed on the resource because a dependency for the action failed.
        /// </summary>
        FailedDependency = 16424,

        /// <summary>
        /// Error code representing HTTP Status Code of 425 for the Lobby Service.
        /// The server is unwilling to risk processing a request that may be replayed.
        /// </summary>
        TooEarly = 16425,

        /// <summary>
        /// Error code representing HTTP Status Code of 426 for the Lobby Service.
        /// The server refuses to perform the request using the current protocol.
        /// </summary>
        UpgradeRequired = 16426,

        /// <summary>
        /// Error code representing HTTP Status Code of 428 for the Lobby Service.
        /// The server requires the request to be conditional.
        /// </summary>
        PreconditionRequired = 16428,

        /// <summary>
        /// Error code representing HTTP Status Code of 429 for the Lobby Service.
        /// Too many requests have been sent in a given amount of time. Please see: https://docs.unity.com/lobby/Content/rate-limits.htm for more details.
        /// </summary>
        RateLimited = 16429,

        /// <summary>
        /// Error code representing HTTP Status Code of 431 for the Lobby Service.
        /// The request has been refused because its HTTP headers are too long.
        /// </summary>
        RequestHeaderFieldsTooLarge = 16431,

        /// <summary>
        /// Error code representing HTTP Status Code of 451 for the Lobby Service.
        /// The requested resource is not available for legal reasons.
        /// </summary>
        UnavailableForLegalReasons = 16451,

        /// <summary>
        /// Error code representing HTTP Status Code of 500 for the Lobby Service.
        /// The server encountered an unexpected condition which prevented it from fulfilling the request.
        /// </summary>
        InternalServerError = 16500,

        /// <summary>
        /// Error code representing HTTP Status Code of 501 for the Lobby Service.
        /// The server does not support the functionality required to fulfil the request.
        /// </summary>
        NotImplemented = 16501,

        /// <summary>
        /// Error code representing HTTP Status Code of 502 for the Lobby Service.
        /// The server, while acting as a gateway or proxy, received an invalid response from the upstream server.
        /// </summary>
        BadGateway = 16502,

        /// <summary>
        /// Error code representing HTTP Status Code of 503 for the Lobby Service.
        /// The server is currently unable to handle the request due to a temporary reason.
        /// </summary>
        ServiceUnavailable = 16503,

        /// <summary>
        /// Error code representing HTTP Status Code of 504 for the Lobby Service.
        /// The server, while acting as a gateway or proxy, did not get a response in time from the upstream server that it needed in order to complete the request.
        /// </summary>
        GatewayTimeout = 16504,

        /// <summary>
        /// Error code representing HTTP Status Code of 505 for the Lobby Service.
        /// The server does not support the HTTP protocol that was used in the request.
        /// </summary>
        HttpVersionNotSupported = 16505,

        /// <summary>
        /// Error code representing HTTP Status Code of 506 for the Lobby Service.
        /// The server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself, and is therefore not a proper end point in the negotiation process.
        /// </summary>
        VariantAlsoNegotiates = 16506,

        /// <summary>
        /// Error code representing HTTP Status Code of 507 for the Lobby Service.
        /// The server has insufficient storage space to complete the request.
        /// </summary>
        InsufficientStorage = 16507,

        /// <summary>
        /// Error code representing HTTP Status Code of 508 for the Lobby Service.
        /// The server terminated the request because it encountered an infinite loop.
        /// </summary>
        LoopDetected = 16508,

        /// <summary>
        /// Error code representing HTTP Status Code of 510 for the Lobby Service.
        /// The policy for accessing the resource has not been met in the request.
        /// </summary>
        NotExtended = 16510,

        /// <summary>
        /// Error code representing HTTP Status Code of 511 for the Lobby Service.
        /// The request requires authentication for network access.
        /// </summary>
        NetworkAuthenticationRequired = 16511,

        /// <summary>
        /// Error code representing a LobbyEvent error for the Lobby Service.
        /// You are already subscribed to this lobby and have attempted to subscribe to it again.
        /// </summary>
        AlreadySubscribedToLobby = 16601,

        /// <summary>
        /// Error code representing a LobbyEvent error for the Lobby Service.
        /// You are already unsubscribed from this lobby and have attempted to unsubscribe from it again.
        /// </summary>
        AlreadyUnsubscribedFromLobby = 16602,

        /// <summary>
        /// Error code representing a LobbyEvent error for the Lobby Service.
        /// The connection was lost or dropped while attempting to do something with the connection such as subscribe or unsubscribe.
        /// </summary>
        SubscriptionToLobbyLostWhileBusy = 16603,

        /// <summary>
        /// Error code representing a LobbyEvent error for the Lobby Service.
        /// Something went wrong when trying to connect to the Lobby service. Ensure a valid Lobby ID was sent.
        /// </summary>
        LobbyEventServiceConnectionError = 16604,

        #endregion

        /// <summary>
        /// NetworkError is returned when the UnityWebRequest failed with this flag set. See the exception stack trace when this reason is provided for context.
        /// </summary>
        NetworkError = 16998,

        /// <summary>
        /// Unknown is returned when a unrecognized error code is returned by the service. Check the inner exception to get more information.
        /// </summary>
        Unknown = 16999
    }
}
