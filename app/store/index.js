export const state = () => ({
    counter: 0,
    inputValue:'Dữ liệu từ store'
  })
  
  export const mutations = {
    increment(state) {
      state.counter++
    },
    updateInputValue(state,value){
        state.inputValue = value
    }
  }
  
  export const actions = {
    increment({ commit }) {
      commit('increment')
    }
  }
  
  export const getters = {
    counter: state => state.counter
  }
  