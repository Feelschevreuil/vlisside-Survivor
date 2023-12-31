
# Rebase

Basé sur la réponse de heinsenberg [ici](https://stackoverflow.com/questions/37709298/how-to-get-changes-from-another-branch) et le push final [ici](https://stackoverflow.com/questions/20467179/git-push-rejected-non-fast-forward).

Afin d'importer les commits d'une autre branche dans celle que vous êtes, 
veuillez utiliser git `rebase`.

Rebase va prendre tous les commits de la branche à importer et les appliquera à votre branche.

1. Vérifiez que vous êtes dans la bonne branche. Nous allons appeler votre branche actuelle `<branche-originale>`. Si non, changez à la branche appropriée.
```
$ git branch
```

2. Spécifiez que vous allez utiliser la fonctionnalité `rebase` pour importer les commits.
```
$ git config pull.rebase true
```

3. Effectuez l'importantion des commits.
```
$ git pull origin <branche-importer-commits>
```
4. Désactivez le pull `rebase` sur la branche de votre choix.
```
$ git config pull.rebase false
```

5. Reconfigurez la branche sur laquelle vous voulez faire des pulls.
```
$ git branch --set-upstream-to=origin/<branche-originale>
```

6. Poussez l'importation des commit au serveur git. (Puisque l'importation des commits a uniquement été fait localement.)
```
$ git push -f -u origin <branche-originale>
```
