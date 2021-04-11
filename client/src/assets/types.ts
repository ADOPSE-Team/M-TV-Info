export interface FAVOURITE_model {
    iD: string;
    mEDIA_ID: string;
    uSER_ID: string;
    dATE_CREATED: string;
}
export interface RATING_model {
    iD: string;
    mEDIA_ID: string;
    uSER_ID: string;
    rATE: number;
    dATE_CREATED: string;
}
export interface ROLE_model {
    iD: string;
    nAME: string;
    dATE_CREATED: string;
}
export interface MEDIA_LOOKUP_model {
    iD: string;
    mEDIA_ID: number;
    nAME: string;
    pOSTER_URL: string;
}
export interface USER_model {
    iD: string;
    uSERNAME: string;
    rOLE_ID: string;
    eMAIL: string;
    bIRTH: string;
    cOUNTRY: string;
    dATE_CREATED: string;
}
export interface WATCHLIST_model {
    iD: string;
    uSER_ID: string;
    nAME: string;
    dATE_CREATED: string;
}
export interface WATCHLIST_ITEM_model {
    iD: string;
    wATCHLIST_ID: string;
    mEDIA_ID: string;
    dATE_CREATED: string;
}