export interface IApiResponse<T> {
  data: T;
  statusCode: number;
  message: string;
}
