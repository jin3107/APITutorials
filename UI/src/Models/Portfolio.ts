export type PortfolioGet = {
    id: number;
    symbol: string;
    companyName: string;
    purchase: number;
    industry: number;
    marketCap: number;
    comments: any;
}

export type PortfolioPost = {
    symbol: string;
}