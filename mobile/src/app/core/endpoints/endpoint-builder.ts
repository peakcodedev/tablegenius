import { environment } from "../../../environments/environment";

export class EndpointBuilder {
  static readonly apiBaseUrl = environment.apiUrl;
  static readonly delimiter = "/";

  static buildDomainEndpoint(domain: string): string {
    return EndpointBuilder.apiBaseUrl + EndpointBuilder.delimiter + domain;
  }
}
