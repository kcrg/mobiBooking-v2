class TokenModel {
  final String _token;

  TokenModel(this._token);

  factory TokenModel.fromJson(Map<String, dynamic> json) {
    return TokenModel(
      json['token'],
    );
  }

  String get token => _token;
}
