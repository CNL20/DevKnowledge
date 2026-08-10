// Khung HTTP client - centralize base URL, interceptor cho JWT/refresh token.
// Implement chi tiết ở Part 3 (feature: Authentication integration).
export interface HttpClientConfig {
  baseUrl: string;
}

export class HttpClient {
  constructor(private config: HttpClientConfig) {}
  // get/post/put/delete methods -> implement ở Part 3
}
