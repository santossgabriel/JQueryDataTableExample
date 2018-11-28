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

const format = (p) => {
  return `<b>Full name:</b> ${p.name}<br />
    <b>Experience:</b> ${p.experience}<br />
    <b>Height:</b> ${p.height}<br />
    <b>Weight:</b> ${p.weight}<br />
    <b>Types:</b> ${p.types.join(', ')}<br />
    <b>Abilities:</b> ${p.abilities.join(', ')}`
}


$(document).ready(() => {

  let detailedRows = {}

  var $table = $('#dataGrid')
  var dataTable = $table.DataTable({
    searchDelay: 3000,
    processing: true,
    serverSide: true,
    ajax: {
      url: "/api/pokemon",
      data: function (d) {
        detailedRows = {}

        const order = d.columns[d.order[0].column].data

        d.nome = 'Fulaninho'
        d.orderBy = `${order[0].toUpperCase()}${order.substring(1)}`
        d.orderDir = d.order[0].dir
        d.searchText = d.search.value
        return d
      }
    },
    columns: [
      { data: "id", class: 'data-id' },
      { data: "name" },
      { data: "experience" },
      { data: "height" },
      { data: "weight" },
      { data: undefined, "defaultContent": '<input type="text" value="0" size="10"/>' }
    ]
  })

  const detailLine = ($tr) => {
    const $newTable = $('<table></table>')
    const $newTr = $tr.clone()
    let html = '<table><tr>'
    let width = 0
    const $tds = $tr.children();
    for (let i = 0; i < $tds.length; i++) {
      const $td = $($tds[i])
      const currentWidth = Number($td.css('width').replace('px', ''))
      html += `<td style="width:${currentWidth}px">${$td.text()}</td>`
      console.log(currentWidth)
      width += currentWidth
    }
    // $newTr.css('width', `${width}px`)
    // $newTable.append($tr.clone())
    return html += '</tr></table>'// $newTable.html()
  }

  let showing = true  

  const adaptRows = () => {
    for (const key in detailedRows) {
      const value = detailedRows[key]
      if (value) {
        value.row.child(detailLine(value.$tr)).show()
      }
    }
  }

  $('#show-hide').click(() => {
    showing = !showing
    dataTable.columns(0).visible(showing)
    adaptRows()
  })

  $table.find('tbody').on('click', 'tr', function () {
    var $tr = $(this).closest('tr')
    var row = dataTable.row($tr)
    var data = row.data()
    if (row.child.isShown()) {
      row.child.hide()
      detailedRows[data.id] = undefined
    } else {
      row.child(detailLine($tr)).show() //row.child(format($row.data())).show()
      detailedRows[data.id] = { row: row, data: data, $tr: $tr }
    }
    console.log(detailedRows)
  })
})