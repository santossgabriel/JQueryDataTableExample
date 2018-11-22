// https://pokeapi.co/api/v2/pokemon/1/

let interval = null

const buscarPokemon = (id) => {
  $.ajax({
    method: 'get',
    url: `api/pokemon/${id}`
  }).then(po => {
    if (!po)
      $.ajax({
        method: 'get',
        url: `https://pokeapi.co/api/v2/pokemon/${id}/`
      }).then(p => {
        const pokemon = {
          abilities: p.abilities.map(x => x.ability.name),
          experience: p.base_experience,
          height: p.height,
          id: p.id,
          name: p.name,
          weight: p.weight,
          stats: p.stats.map(x => ({ total: x.base_stat, name: x.stat.name, effort: x.effort })),
          types: p.types.map(x => x.type.name)
        }
        pokemon.sprites = []
        for (const s in p.sprites)
          if (p.sprites[s])
            pokemon.sprites.push(p.sprites[s])

        $.ajax({
          method: 'post',
          url: `api/pokemon/`,
          data: pokemon
        }).then(() => console.log(`Pokemon ${pokemon.id} - ${pokemon.name} inserido!`))
          .catch(err => {
            clearInterval(interval)
            console.log(err)
          })
      }).catch(err => {
        clearInterval(interval)
        console.log(err)
      })
  }).catch(err => {
    clearInterval(interval)
    console.log(err)
  })
}
// let id = 700
// interval = setInterval(() => {
//   buscarPokemon(id)
//   id++
//   if (id > 805)
//     clearInterval(interval)
// }, 1000)


$(document).ready(() => {
  $('#dataGrid').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": {
      url: "/api/pokemon",
      dataSrc: (json) => {
        console.log(json)
        return json
      }
    },
    "columns": [
      { "data": "id" },
      { "data": "name" },
      { "data": "experience" },
      { "data": "height" },
      { "data": "weight" }
    ]
  });
})