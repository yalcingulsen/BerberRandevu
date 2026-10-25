import 'package:flutter/material.dart';

void main() {
  runApp(const BerberRandevuApp());
}

class BerberRandevuApp extends StatelessWidget {
  const BerberRandevuApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Berber Randevu',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        colorSchemeSeed: Colors.brown,
        useMaterial3: true,
      ),
      home: const KarsilamaEkrani(),
    );
  }
}

class KarsilamaEkrani extends StatelessWidget {
  const KarsilamaEkrani({super.key});

  void _randevuAlTiklandi(BuildContext context) {
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        content: Text('Randevu alma ekranı sonraki haftalarda eklenecek.'),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final renkler = Theme.of(context).colorScheme;

    return Scaffold(
      body: SafeArea(
        child: Center(
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 420),
            child: Padding(
              padding: const EdgeInsets.all(24),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  CircleAvatar(
                    radius: 56,
                    backgroundColor: renkler.primaryContainer,
                    child: Icon(
                      Icons.content_cut,
                      size: 56,
                      color: renkler.onPrimaryContainer,
                    ),
                  ),
                  const SizedBox(height: 24),
                  Text(
                    'Berber Randevu',
                    style: Theme.of(context).textTheme.headlineMedium?.copyWith(
                          fontWeight: FontWeight.bold,
                        ),
                  ),
                  const SizedBox(height: 12),
                  const Text(
                    'Sıra beklemeden, telefonundan berber randevusu al.',
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 32),
                  SizedBox(
                    width: double.infinity,
                    child: FilledButton.icon(
                      onPressed: () => _randevuAlTiklandi(context),
                      icon: const Icon(Icons.calendar_month),
                      label: const Text('Randevu Al'),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
