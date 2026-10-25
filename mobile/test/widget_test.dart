import 'package:flutter_test/flutter_test.dart';
import 'package:berber_randevu/main.dart';

void main() {
  testWidgets('Karşılama ekranı açılıyor', (tester) async {
    await tester.pumpWidget(const BerberRandevuApp());
    expect(find.text('Berber Randevu'), findsOneWidget);
    expect(find.text('Randevu Al'), findsOneWidget);
  });
}
